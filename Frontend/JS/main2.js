let nameDoctor = document.querySelector(".doctor-info");
import { doctor } from './doctorObj.js';
const savedDoctor = JSON.parse(localStorage.getItem('doctor'));
if (savedDoctor) {
    doctor.name = savedDoctor.name;
    doctor.lastName = savedDoctor.lastName;
    doctor.age = savedDoctor.age;
    doctor.id = savedDoctor.id || null; // Додаємо ID лікаря, якщо він є
}
const info = doctor.name + " " + doctor.lastName + ", " + doctor.age + " років" + " ID "+ doctor.id ;
nameDoctor.textContent = "Лікар: " + info;
console.log("Інформація про лікаря: " + info);


function getDoctors() {
    fetch('http://localhost:5098/api/doctors')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Список лікарів:', data);
            const doctorsTableBody = document.querySelector('#doctorsTable tbody');
            doctorsTableBody.innerHTML = ''; // Очищаємо попередні дані
            data.forEach(doctor => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${doctor.name}</td>
                    <td>${doctor.lastName}</td>
                    <td>${doctor.age}</td>
                `;
                doctorsTableBody.appendChild(tr);
            });
        })
        .catch(error => {
            console.error('Помилка при отриманні списку лікарів:', error);
        });
}
//getDoctors()

function getMedications() {
    fetch('http://localhost:5098/api/medication')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Список медикаментів:', data);
            const doctorsTableBody = document.querySelector('#medicationTable tbody');
            doctorsTableBody.innerHTML = ''; // Очищаємо попередні дані
            data.forEach(doctor => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${doctor.name}</td>
                    <td>${doctor.description}</td>
                    <td>${doctor.quantity}</td>
                `;
                doctorsTableBody.appendChild(tr);
            });
        })
        .catch(error => {
            console.error('Помилка при отриманні списку лікарів:', error);
        });
}
//getMedications()
function getPations() {
    fetch('http://localhost:5098/api/patients')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Список пацієнтів:', data);
            const doctorsTableBody = document.querySelector('#patientsTable tbody');
            doctorsTableBody.innerHTML = ''; // Очищаємо попередні дані
            data.forEach(doctor => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${doctor.name}</td>
                    <td>${doctor.lastName}</td>
                    <td>${doctor.age}</td>
                `;
                doctorsTableBody.appendChild(tr);
            });

            // Додаємо обробник подій після створення рядків
            const modal = document.getElementById('modalOverlay');
            const nameH1 = document.getElementById('modalPatientName');
            const nameSpan = document.getElementById('modalPatientName2');
            const patientRows = doctorsTableBody.querySelectorAll('tr');
            patientRows.forEach((row,index) => {
                row.addEventListener('click', () => {
                    const name = row.children[0].textContent + ' ' + row.children[1].textContent;
                    nameH1.textContent = name;
                    if (nameSpan) nameSpan.textContent = name;

                    console.log(`Клік по пацієнту ${index + 1}: ${name}`);
                    getInformation(index);
                    modal.classList.remove('hidden');
                });
            });
               modal.addEventListener('click', (e) => {
            if (e.target === modal) {
                modal.classList.add('hidden');
            }
    });
        })
        .catch(error => {
            console.error('Помилка при отриманні списку лікарів:', error);
        });
}
//getPations()

document.addEventListener('DOMContentLoaded', function() {
  // Ваш код, який виконається при завантаженні сторінки
  console.log('Сторінка завантажена!');
  getDoctors();
  getMedications();
  getPations();
});
//----------------------
// function getInformation(index){
//     const d = document.querySelector('#Descriptions');
//     const nameD = document.querySelector('#nameD');
//     const desD = document.querySelector('#desD');
//     const dt = document.querySelector('#dt');
//     const tt = document.querySelector('#tt');
//     const doctorName = document.querySelector('#doctorName');
//     const patientName = document.querySelector('#patientName');
//     try {
//         fetch(`http://localhost:5098/api/informationAboutUser/${index}`)
//         .then(response => {
//             if (!response.ok) {
//                 throw new Error('Network response was not ok');
//             }
//             return response.json();
//         })
//         .then(data => {
//             console.log(data);
//             d.textContent = data.description;
//             nameD.textContent = data.nameDisease;
//             desD.textContent = data.descriptionDisease;
//             // dt.textContent = new Date(data.visitDate).toLocaleDateString();
//              const dateObj = new Date(data.visitDate);
//             dt.textContent = dateObj.toLocaleDateString('uk-UA'); // дата
//             tt.textContent = dateObj.toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' }); // час

//             doctorName.textContent = `${data.doctor.name} ${data.doctor.lastName}, ${data.doctor.age} років`;
//         })
//     }
//     catch (error) {
//         console.error('Помилка при отриманні інформації про пацієнта:', error);
//     }
// }
// {
//   "description": "Типові ознаки інсулінорезистентності. Діабет II типу.",
//   "descriptionDisease": "Порушення обміну глюкози з інсулінорезистентністю.",
//   "nameDisease": "Цукровий діабет II типу",
//   "visitDate": "2025-02-08T00:00:00",
//   "doctor": {
//     "id": "8447dfe1-87c2-4a97-0ad1-08ddb4cb87d2",
//     "name": "Олександр",
//     "lastName": "Тимошенко",
//     "age": 39,
//     "login": "o.timosh",
//     "password": "Doc12345!"
//   },
//   "patient": {
//     "id": "54ef92c9-4de3-4ead-90a6-08ddb4cb87dc",
//     "name": "Ірина",
//     "lastName": "Петренко",
//     "age": 27
//   }
// }


function getInformation(index) {
    const modalContent = document.querySelector('.modal-content section');
    const nameSpan = document.getElementById('modalPatientName2');
    if (modalContent) modalContent.innerHTML = '';

    fetch(`http://localhost:5098/api/informationAboutUser/${index}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (nameSpan) nameSpan.textContent = `${data.patient.name} ${data.patient.lastName}`;

            // Перевіряємо чи є масиви і чи вони однакової довжини
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
                        <strong>Відвідуваннячу:</strong>
                        <ul>
                            <li><strong>Дата: </strong>${dateObj.toLocaleDateString('uk-UA')}</li>
                            <li><strong>Час: </strong>${dateObj.toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' })}</li>
                            <li><strong>Імя лікаря: </strong>${doctor.name} ${doctor.lastName}, ${doctor.age} років</li>
                            <li><strong>Імя пацієнта: </strong>${data.patient.name} ${data.patient.lastName}</li>
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


document.addEventListener('DOMContentLoaded', function() {
    // ...existing code...
    // Side menu
    const menuBtn = document.querySelector('.menu-button');
    const sideMenu = document.getElementById('sideMenu');
    const closeBtn = document.getElementById('closeMenu');
    if (menuBtn && sideMenu && closeBtn) {
        menuBtn.addEventListener('click', () => {
            sideMenu.classList.add('show');
            sideMenu.classList.remove('hidden');
        });
        closeBtn.addEventListener('click', () => {
            sideMenu.classList.remove('show');
            sideMenu.classList.add('hidden');
        });
        // Закриття по кліку поза меню
        document.addEventListener('mousedown', (e) => {
            if (sideMenu.classList.contains('show') && !sideMenu.contains(e.target) && !menuBtn.contains(e.target)) {
                sideMenu.classList.remove('show');
                sideMenu.classList.add('hidden');
            }
        });
    }
});