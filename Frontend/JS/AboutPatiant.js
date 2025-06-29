window.addEventListener('DOMContentLoaded', () => {
    // твій код тут

    const modal = document.getElementById('modalOverlay');
    const nameH1 = document.getElementById('modalPatientName');
    const nameSpan = document.getElementById('modalPatientName2');
    const patientRows = document.querySelectorAll('#patientsTable tbody tr');
        console.log(`Знайдено пацієнтів`);

    patientRows.forEach(row => {
        console.log(`Знайдено ${patientRows.length} пацієнтів`);
        console.log(`Знайдено пацієнтів`);
        row.addEventListener('click', () => {
            console.log(`Клік по пацієнту ${row}`);
            const name = row.children[0].textContent + ' ' + row.children[1].textContent;
            nameH1.textContent = name;
            if (nameSpan) nameSpan.textContent = name;
            modal.classList.remove('hidden');
        });
    });

 
});
