import { doctor } from './JS/doctorObj.js'
document.addEventListener('DOMContentLoaded', () => {
    // Відображення лікаря (якщо потрібно)
    const savedDoctor = JSON.parse(localStorage.getItem('doctor'));
    if (savedDoctor) {
        doctor.name = savedDoctor.name;
        doctor.lastName = savedDoctor.lastName;
        doctor.age = savedDoctor.age;
        doctor.id = savedDoctor.id || null;
    }
    const info = doctor.name + " " + doctor.lastName + ", " + doctor.age + " років" + " ID "+ doctor.id ;
    const doctorInfo = document.querySelector('.doctor-info');
    if (doctorInfo) doctorInfo.textContent = "Лікар: " + info;

    // Завантаження медикаментів
    const tableBody = document.querySelector('#medicationTable tbody');
    let selectedRow = null;
    let medications = [];

    function loadMedications() {
        fetch('http://localhost:5098/api/medication')
            .then(r => r.json())
            .then(data => {
                medications = data;
                console.log(data);
                tableBody.innerHTML = '';
                data.forEach((m, i) => {
                    const tr = document.createElement('tr');
                    tr.innerHTML = `<td>${m.name}</td><td>${m.description}</td><td>${m.quantity}</td>`;
                    tr.onclick = () => selectRow(tr, i);
                    tableBody.appendChild(tr);
                });
                disableActions();
            });
    }

    function selectRow(tr, idx) {
        if (selectedRow) selectedRow.classList.remove('selected');
        selectedRow = tr;
        selectedRow.classList.add('selected');
        document.getElementById('editMedicationBtn').disabled = false;
        document.getElementById('deleteMedicationBtn').disabled = false;
        selectedRow.dataset.idx = idx;
    }

    function disableActions() {
        document.getElementById('editMedicationBtn').disabled = true;
        document.getElementById('deleteMedicationBtn').disabled = true;
        if (selectedRow) selectedRow.classList.remove('selected');
        selectedRow = null;
    }

    // Додати медикамент
    document.getElementById('addMedicationBtn').onclick = () => {
        const name = prompt("Назва медикаменту:");
        const description = prompt("Опис медикаменту:");
        const count = prompt("Кількість:");
        if (name && description && count) {
            fetch('http://localhost:5098/api/medication', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, description, quantity: Number(count) })
            }).then(loadMedications);
        }
    };

    // Редагувати медикамент
    document.getElementById('editMedicationBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const med = medications[idx];
        const name = prompt("Назва медикаменту:", med.name);
        const description = prompt("Опис медикаменту:", med.description);
        const count = prompt("Кількість:", med.quantity);
        if (name && description && count) {
            fetch(`http://localhost:5098/api/medication/${med.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, description, quantity: Number(count) })
            }).then(loadMedications);
        }
    };

    // Видалити медикамент
    document.getElementById('deleteMedicationBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const med = medications[idx];
        fetch(`http://localhost:5098/api/medication/${med.id}`, {
            method: 'DELETE'
        }).then(loadMedications);
    };

    // Сортування
    const sortedFields = ["Name", "Description", "Quantity","Simply"];
    let sortIndex = 0;
    document.getElementById('sortMedicationBtn').onclick = () => {
        sortIndex++;
        if (sortIndex >= sortedFields.length) sortIndex = 0;
        const sortField = sortedFields[sortIndex];
        fetch(`http://localhost:5098/api/medication/sorted?sortedBy=${sortField}`, {
            method: 'GET'
        })
        .then(response => response.json())
        .then(data => {
            medications = data;
            tableBody.innerHTML = '';
            data.forEach((m, i) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${m.name}</td><td>${m.description}</td><td>${m.quantity}</td>`;
                tr.onclick = () => selectRow(tr, i);
                tableBody.appendChild(tr);
            });
            disableActions();
        })
        .catch(err => alert(err.message));
    };

    // Модальне вікно (інформація, якщо потрібно)
    document.getElementById('modalOverlay').addEventListener('click', (e) => {
        if (e.target.id === 'modalOverlay') {
            document.getElementById('modalOverlay').classList.add('hidden');
        }
    });

    loadMedications();
});