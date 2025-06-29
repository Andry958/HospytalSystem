import { doctor } from './doctorObj.js';

document.addEventListener('DOMContentLoaded', () => {
    // Відображення лікаря
    const savedDoctor = JSON.parse(localStorage.getItem('doctor'));
    if (savedDoctor) {
        doctor.name = savedDoctor.name;
        doctor.lastName = savedDoctor.lastName;
        doctor.age = savedDoctor.age;
        doctor.id = savedDoctor.id || null;
    }
    const info = doctor.name + " " + doctor.lastName + ", " + doctor.age + " років" + " ID "+ doctor.id ;
    document.querySelector('.doctor-info').textContent = "Лікар: " + info;

    // Завантаження пацієнтів
    const tableBody = document.querySelector('#patientsTable tbody');
    let selectedRow = null;
    let patients = [];

    function loadPatients() {
        fetch('http://localhost:5098/api/patients')
            .then(r => r.json())
            .then(data => {
                patients = data;
                tableBody.innerHTML = '';
                data.forEach((p, i) => {
                    const tr = document.createElement('tr');
                    tr.innerHTML = `<td>${p.name}</td><td>${p.lastName}</td><td>${p.age}</td>`;
                    tr.addEventListener('click', () => selectRow(tr, i));
                    tableBody.appendChild(tr);
                });
                disableActions();
            });
    }

    function selectRow(tr, idx) {
        if (selectedRow) selectedRow.classList.remove('selected');
        selectedRow = tr;
        selectedRow.classList.add('selected');
        document.getElementById('editBtn').disabled = false;
        document.getElementById('deleteBtn').disabled = false;
        document.getElementById('infoBtn').disabled = false;
        selectedRow.dataset.idx = idx;
    }

    function disableActions() {
        document.getElementById('editBtn').disabled = true;
        document.getElementById('deleteBtn').disabled = true;
        document.getElementById('infoBtn').disabled = true;
        if (selectedRow) selectedRow.classList.remove('selected');
        selectedRow = null;
    }

    // Додати пацієнта
    document.getElementById('addBtn').onclick = () => {
        const name = prompt("Ім'я пацієнта:");
        const lastName = prompt("Прізвище пацієнта:");
        const age = prompt("Вік пацієнта:");
        if (name && lastName && age) {
            fetch('http://localhost:5098/api/patients', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, lastName, age: Number(age) })
            }).then(loadPatients);
        }
    };

    // Редагувати пацієнта
    document.getElementById('editBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const patient = patients[idx];
        const name = prompt("Ім'я пацієнта:", patient.name);
        const lastName = prompt("Прізвище пацієнта:", patient.lastName);
        const age = prompt("Вік пацієнта:", patient.age);
        if (name && lastName && age) {
            fetch(`http://localhost:5098/api/patientsEd/${patient.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    Name: name,
                    LastName: lastName,
                    Age: Number(age),
                })
            })
            .then(loadPatients);
        }
    };

    // Видалити пацієнта
    document.getElementById('deleteBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const patient = patients[idx];
        fetch(`http://localhost:5098/api/patientsEd/${patient.id}`, {
            method: 'DELETE'
        })
        .then(loadPatients);
    };
    const sortedPatients = ["Name", "LastName", "Age","Simply"];
    let sortIndex = 0;
    document.getElementById('SortedBtn').onclick = () => {
        sortIndex++;
        if (sortIndex >= sortedPatients.length) sortIndex = 0;
        const sortField = sortedPatients[sortIndex];
        console.log(`Сортування за полем: ${sortField}`);
        fetch(`http://localhost:5098/api/patientsEd?sortedBy=${sortField}`, {
            method: 'GET'
        })
        .then(response => {
            if (!response.ok) throw new Error('Помилка при отриманні даних');
            return response.json();
        })
        .then(data => {
            patients = data;
            tableBody.innerHTML = '';
            data.forEach((p, i) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${p.name}</td><td>${p.lastName}</td><td>${p.age}</td>`;
                tr.addEventListener('click', () => selectRow(tr, i));
                tableBody.appendChild(tr);
            });
            disableActions();
        })
        .catch(err => alert(err.message));
    };
    
    // Додати діагноз
    document.getElementById('addDiagnosisBtn').onclick = () => {
        if (!selectedRow) {
            alert('Оберіть пацієнта для додавання діагнозу!');
            return;
        }
        document.getElementById('diagnosisModal').classList.remove('hidden');
    };

    document.getElementById('closeDiagnosisModal').onclick = () => {
        document.getElementById('diagnosisModal').classList.add('hidden');
        document.getElementById('diagnosisForm').reset();
    };

    document.getElementById('diagnosisForm').onsubmit = (e) => {
        e.preventDefault();
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        const patient = patients[idx];

        const form = e.target;
        const data = {
            description: form.description.value,
            descriptionDisease: form.descriptionDisease.value,
            nameDisease: form.nameDisease.value,
            visitDate: form.visitDate.value,
            doctorId: doctor.id ,
            patientId: patient.id
        };
        console.log('Дані для додавання діагнозу:', data);

        fetch('http://localhost:5098/api/patientsEd/diagnosis', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        })
        .then(r => {
            if (!r.ok) throw new Error('Помилка при додаванні діагнозу');
            return r.json();
        })
        .then(() => {
            document.getElementById('diagnosisModal').classList.add('hidden');
            form.reset();
            alert('Діагноз додано!');
        })
        .catch(err => alert(err.message));
    };

    // Інформація про пацієнта
    document.getElementById('infoBtn').onclick = () => {
        if (!selectedRow) return;
        const idx = selectedRow.dataset.idx;
        getInformation(idx);
        const modal = document.getElementById('modalOverlay');
        modal.classList.remove('hidden');
    };

    document.getElementById('modalOverlay').addEventListener('click', (e) => {
        if (e.target.id === 'modalOverlay') {
            e.target.classList.add('hidden');
        }
    });

    loadPatients();
});

// Функція для отримання інформації про пацієнта
function getInformation(index) {
    const modalContent = document.querySelector('.modal-content section');
    const nameSpan = document.getElementById('modalPatientName');
    if (modalContent) modalContent.innerHTML = '';

    fetch(`http://localhost:5098/api/informationAboutUser/${index}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (!data || !data.patient) {
                modalContent.innerHTML = '<p>Діагнози відсутні.</p>';
                console.error('Немає даних про пацієнта');
                return;
            }
            if (nameSpan) nameSpan.textContent = `${data.patient.name} ${data.patient.lastName}`;

            const count = Array.isArray(data.description) ? data.description.length : 0;
            for (let i = 0; i < count; i++) {
                const dateObj = new Date(data.visitDate[i]);
                const doctor = data.doctor[i];
                const section = document.createElement('section');
                section.innerHTML = `
                    <h2>Діагноз</h2>
                    <div>
                        <strong>Опис:</strong>
                        <p>${data.description[i]}</p>
                    </div>
                    <div>
                        <strong>Хвороба:</strong>
                        <ul>
                            <li><strong>Назва: </strong>${data.nameDisease[i]}</li>
                            <li><strong>Опис: </strong>${data.descriptionDisease[i]}</li>
                        </ul>
                    </div>
                    <div>
                        <strong>Відвідування:</strong>
                        <ul>
                            <li><strong>Дата: </strong>${dateObj.toLocaleDateString('uk-UA')}</li>
                            <li><strong>Час: </strong>${dateObj.toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' })}</li>
                            <li><strong>Ім'я лікаря: </strong>${doctor.name} ${doctor.lastName}, ${doctor.age} років</li>
                            <li><strong>Ім'я пацієнта: </strong>${data.patient.name} ${data.patient.lastName}</li>
                        </ul>
                        <hr/>
                    </div>
                `;
                if (modalContent) modalContent.appendChild(section);
            }
            if (count === 0 && modalContent) {
                modalContent.innerHTML = '<p>Діагнози відсутні.</p>';
            }
        })
        .catch(error => {
            console.error('Помилка при отриманні інформації про пацієнта:', error);
        });

    
}