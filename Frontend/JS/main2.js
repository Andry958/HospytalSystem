let nameDoctor = document.querySelector(".doctor-info");
import { doctor } from './doctorObj.js';
const savedDoctor = JSON.parse(localStorage.getItem('doctor'));
if (savedDoctor) {
    doctor.name = savedDoctor.name;
    doctor.lastName = savedDoctor.lastName;
    doctor.age = savedDoctor.age;
}
const info = doctor.name + " " + doctor.lastName + ", " + doctor.age + " років";
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
getDoctors()