const loginForm = document.getElementById('loginForm');
const registerForm = document.getElementById('registerForm');
const showLoginBtn = document.getElementById('showLogin');
const showRegisterBtn = document.getElementById('showRegister');
import { doctor } from './doctorObj.js';

showLoginBtn.addEventListener('click', () => {
  loginForm.style.display = 'block';
  registerForm.style.display = 'none';
});

showRegisterBtn.addEventListener('click', () => {
  loginForm.style.display = 'none';
  registerForm.style.display = 'block';
});

loginForm.addEventListener('submit', async e => {
  e.preventDefault();
  const login = loginForm.querySelector('#loginLogin').value.trim();
  const password = loginForm.querySelector('#loginPassword').value.trim();
  console.log(`Авторизація:\nЛогін: ${login}\nПароль: ${password}`);

  try {
    const response = await fetch('http://localhost:5098/api/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        login,
        password
      })
    });

    if (!response.ok) {
      const errorData = await response.json();
      alert(`Помилка входу: ${errorData.message || response.statusText}`);
      return;
    }

    const data = await response.json();
    console.log('Успішний вхід:', data);
    alert(`Вхід успішний! Вітаємо, ${data.age}`);
    doctor.age = data.age;
    doctor.name = data.name;
    doctor.lastName = data.lastName;
    localStorage.setItem('doctor', JSON.stringify({
      name: doctor.name,
      lastName: doctor.lastName,
      age: doctor.age
    }));
    console.log(`Інформація про лікаря: ${doctor.name} ${doctor.lastName}, ${doctor.age} років`);
    loginForm.reset();
    window.location.href = "index2.html";
  } catch (error) {
    alert('Сталася помилка при вході. Спробуйте ще раз.');
    console.error(error);
  }
});
//---------------------------------------
registerForm.addEventListener('submit', async e => {
  e.preventDefault();
  const name = registerForm.querySelector('#regName').value.trim();
  const lastName = registerForm.querySelector('#regLastName').value.trim();
  const age = parseInt(registerForm.querySelector('#regAge').value.trim(), 10);
  const login = registerForm.querySelector('#regLogin').value.trim();
  const password = registerForm.querySelector('#regPassword').value.trim();
  console.log(`Реєстрація:\nІм'я: ${name}\nПрізвище: ${lastName}\nВік: ${age}\nЛогін: ${login}\nПароль: ${password}`);

  try {
    const response = await fetch('http://localhost:5098/api/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        name,
        lastName,
        age,
        login,
        password
      })
    });

    if (!response.ok) {
      const errorData = await response.json();
      alert(`Помилка реєстрації: ${errorData.message || response.statusText}`);
      return;
    }

    const data = await response.json();
    alert(`Реєстрація успішна! Вітаємо, ${data.name || name}`);
    // Зберігаємо дані лікаря в localStorage
    doctor.age = data.age || age;
    doctor.name = data.name || name;
    doctor.lastName = data.lastName || lastName;
    localStorage.setItem('doctor', JSON.stringify({
      name: doctor.name,
      lastName: doctor.lastName,
      age: doctor.age
    }));
    console.log(`Інформація про лікаря: ${doctor.name} ${doctor.lastName}, ${doctor.age} років`);
    registerForm.reset();
    window.location.href = "index2.html";
  } catch (error) {
    alert('Сталася помилка при реєстрації. Спробуйте ще раз.');
    console.error(error);
  }
});