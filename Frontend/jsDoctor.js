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
    document.querySelector('.doctor-info').textContent = "Лікар: " + info;

    // Завантаження лікарів
    const tableBody = document.querySelector('#doctorsTable tbody');
    let selectedRow = null;
    let doctors = [];

    function loadDoctors() {
        fetch('http://localhost:5098/api/doctors')
            .then(r => r.json())
            .then(data => {
                doctors = data;
                tableBody.innerHTML = '';
                data.forEach((d, i) => {
                    const tr = document.createElement('tr');
                    tr.innerHTML = `<td>${d.name}</td><td>${d.lastName}</td><td>${d.age}</td>`;
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
        document.getElementById('editDoctorBtn').disabled = false;
        document.getElementById('deleteDoctorBtn').disabled = false;
        document.getElementById('infoDoctorBtn').disabled = false;
        selectedRow.dataset.idx = idx;
    }

    function disableActions() {
        document.getElementById('editDoctorBtn').disabled = true;
        document.getElementById('deleteDoctorBtn').disabled = true;
        document.getElementById('infoDoctorBtn').disabled = true;
        if (selectedRow) selectedRow.classList.remove('selected');
        selectedRow = null;
    }

    // Додати лікаря
    document.getElementById('addDoctorBtn').onclick = () => {
        const name = prompt("Ім'я лікаря:");
        const lastName = prompt("Прізвище лікаря:");
        const age = prompt("Вік лікаря:");
        if (name && lastName && age) {
            fetch('http://localhost:5098/api/doctors', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, lastName, age: Number(age) })
            }).then(loadDoctors);
        }
    };

    // Редагувати лікаря
    document.getElementById('editDoctorBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const doctorObj = doctors[idx];
        const name = prompt("Ім'я лікаря:", doctorObj.name);
        const lastName = prompt("Прізвище лікаря:", doctorObj.lastName);
        const age = prompt("Вік лікаря:", doctorObj.age);
        if (name && lastName && age) {
            fetch(`http://localhost:5098/api/doctors/${doctorObj.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, lastName, age: Number(age) })
            }).then(loadDoctors);
        }
    };

    // Видалити лікаря
    document.getElementById('deleteDoctorBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const doctorObj = doctors[idx];
        fetch(`http://localhost:5098/api/doctors/${doctorObj.id}`, {
            method: 'DELETE'
        }).then(loadDoctors);
    };

    // Сортування
    const sortedDoctors = ["Name", "LastName", "Age","Simply"];
    let sortIndex = 0;
    document.getElementById('sortDoctorBtn').onclick = () => {
        sortIndex++;
        if (sortIndex >= sortedDoctors.length) sortIndex = 0;
        const sortField = sortedDoctors[sortIndex];
        fetch(`http://localhost:5098/api/doctors?sortedBy=${sortField}`, {
            method: 'GET'
        })
        .then(response => response.json())
        .then(data => {
            doctors = data;
            tableBody.innerHTML = '';
            data.forEach((d, i) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${d.name}</td><td>${d.lastName}</td><td>${d.age}</td>`;
                tr.onclick = () => selectRow(tr, i);
                tableBody.appendChild(tr);
            });
            disableActions();
        })
        .catch(err => alert(err.message));
    };

    // Інформація про лікаря
    document.getElementById('infoDoctorBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        getDoctorInformation(idx);
        const modal = document.getElementById('modalOverlay');
        modal.classList.remove('hidden');
    };

    document.getElementById('modalOverlay').addEventListener('click', (e) => {
        if (e.target.id === 'modalOverlay') {
            document.getElementById('modalOverlay').classList.add('hidden');
        }
    });

    loadDoctors();
});

// Функція для отримання інформації про лікаря
function getDoctorInformation(index) {
    const modalContent = document.querySelector('.modal-content section');
    const nameSpan = document.getElementById('modalDoctorName');
    if (modalContent) modalContent.innerHTML = '';

    fetch(`http://localhost:5098/api/doctors/${index}`)
        .then(response => {
            if (!response.ok) throw new Error('Помилка при отриманні інформації');
            return response.json();
        })
        .then(data => {
            if (!data) return;
            if (nameSpan) nameSpan.textContent = `${data.name} ${data.lastName}`;
            if (modalContent) {
                modalContent.innerHTML = `
                    <p><b>Вік:</b> ${data.age}</p>
                    <p><b>ID:</b> ${data.id}</p>
                `;
            }
        })
        .catch(error => {
            console.error('Помилка при отриманні інформації про лікаря:', error);
        });
}