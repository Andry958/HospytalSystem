using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Servies
{
    public class AutoFillC_service
    {
        private readonly HospitalDbContext _context;
        public AutoFillC_service(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<List<object>> GetData()
        {
            List<object> data = new List<object>();
            var doctors = await _context.Doctors.ToListAsync();
            var patients = await _context.Patients.ToListAsync();
            var medications = await _context.Medications.ToListAsync();
            var diseases = await _context.Disease.ToListAsync();
            var visits = await _context.Visits.ToListAsync();
            var diagnoses = await _context.Diagnosiss.ToListAsync();

            data.Add(doctors);
            data.Add(patients);
            data.Add(medications);
            data.Add(diseases);
            data.Add(visits);
            data.Add(diagnoses);

            return data;
        }
        public async Task<string> FillData()
        {
            _context.Doctors.RemoveRange(_context.Doctors);
            _context.Patients.RemoveRange(_context.Patients);
            _context.Medications.RemoveRange(_context.Medications);
            _context.SaveChanges();

            var doctors = new List<Doctor>
            {
                new Doctor { Name = "Анна", LastName = "Іваненко", Age = 42, Login = "a.ivanenko", Password = "SecurePa$$123" },
                new Doctor { Name = "Олександр", LastName = "Тимошенко", Age = 39, Login = "o.timosh", Password = "Doc12345!" },
                new Doctor { Name = "Ігор", LastName = "Кравець", Age = 45, Login = "ikravets", Password = "Qwerty@987" },
                new Doctor { Name = "Оксана", LastName = "Бондар", Age = 31, Login = "ox.bondar", Password = "HealthyPass12" },
                new Doctor { Name = "Сергій", LastName = "Левченко", Age = 50, Login = "slevch", Password = "Med!care9" },
                new Doctor { Name = "Лідія", LastName = "Гринюк", Age = 28, Login = "lgryn", Password = "LiDiA2024" },
                new Doctor { Name = "Василь", LastName = "Павлюк", Age = 55, Login = "vpavluk", Password = "Va55iL" },
                new Doctor { Name = "Марина", LastName = "Чумак", Age = 44, Login = "m.chumak", Password = "PassMarya33" },
                new Doctor { Name = "Євген", LastName = "Скрипник", Age = 36, Login = "evgen.scr", Password = "EvGenPass!" },
                new Doctor { Name = "Леся", LastName = "Юрченко", Age = 30, Login = "lesyur", Password = "L@sy123" },
                new Doctor { Name = "Дмитро", LastName = "Жук", Age = 47, Login = "dzhuk", Password = "ZhykSecure1" },
                new Doctor { Name = "Валентина", LastName = "Ігнатенко", Age = 33, Login = "v.ignat", Password = "ValenT33n@" },
                new Doctor { Name = "Микола", LastName = "Герасимчук", Age = 41, Login = "mhera", Password = "MikoSecure!" },
                new Doctor { Name = "Юлія", LastName = "Калініна", Age = 29, Login = "y.kalin", Password = "YKpass@2" },
                new Doctor { Name = "Арсен", LastName = "Федорчук", Age = 38, Login = "arsfed", Password = "F3doR456!" }
            };

            var patients = new List<Patient>
            {
                new Patient { Name = "Олег", LastName = "Мельник", Age = 34 },
                new Patient { Name = "Ірина", LastName = "Петренко", Age = 27 },
                new Patient { Name = "Андрій", LastName = "Ковальчук", Age = 41 },
                new Patient { Name = "Світлана", LastName = "Шевченко", Age = 19 },
                new Patient { Name = "Тарас", LastName = "Гаврилюк", Age = 52 },
                new Patient { Name = "Марія", LastName = "Романенко", Age = 38 },
                new Patient { Name = "Віктор", LastName = "Захарченко", Age = 60 },
                new Patient { Name = "Анастасія", LastName = "Семенюк", Age = 25 },
                new Patient { Name = "Юрій", LastName = "Демчук", Age = 46 },
                new Patient { Name = "Катерина", LastName = "Кирилюк", Age = 33 },
                new Patient { Name = "Богдан", LastName = "Панчук", Age = 29 },
                new Patient { Name = "Олена", LastName = "Мазур", Age = 22 },
                new Patient { Name = "Ростислав", LastName = "Сидоренко", Age = 55 },
                new Patient { Name = "Людмила", LastName = "Ткач", Age = 40 },
                new Patient { Name = "Максим", LastName = "Лисенко", Age = 36 }
            };

            var medications = new List<Medication>
            {
                new Medication { Name = "Парацетамол", Description = "Жарознижувальний", Quantity = 100 },
                new Medication { Name = "Ібупрофен", Description = "Протизапальний засіб", Quantity = 50 },
                new Medication { Name = "Цитрамон", Description = "Знеболювальне", Quantity = 80 },
                new Medication { Name = "Амоксицилін", Description = "Антибіотик", Quantity = 30 },
                new Medication { Name = "Но-шпа", Description = "Спазмолітик", Quantity = 70 },
                new Medication { Name = "Супрастин", Description = "Антигістамінний", Quantity = 60 },
                new Medication { Name = "Фарингосепт", Description = "Антисептик для горла", Quantity = 40 },
                new Medication { Name = "Лоратадин", Description = "Протиалергійний", Quantity = 100 },
                new Medication { Name = "Німесулід", Description = "Протизапальний препарат", Quantity = 35 },
                new Medication { Name = "Ренгалін", Description = "Проти кашлю", Quantity = 90 },
                new Medication { Name = "Кардіомагніл", Description = "Для серця", Quantity = 45 },
                new Medication { Name = "Декангестант", Description = "Проти нежитю", Quantity = 55 },
                new Medication { Name = "Метронідазол", Description = "Антибактеріальний", Quantity = 25 },
                new Medication { Name = "Лінекс", Description = "Пробіотик", Quantity = 75 },
                new Medication { Name = "Цефтріаксон", Description = "Антибіотик широкого спектру", Quantity = 20 }
            };

            var diseases = new List<Disease>
            {
                new Disease { Name = "Гіпертонія", Description = "Хронічне підвищення артеріального тиску, що вимагає постійного контролю." },
                new Disease { Name = "Цукровий діабет II типу", Description = "Порушення обміну глюкози з інсулінорезистентністю." },
                new Disease { Name = "Астма", Description = "Хронічне запальне захворювання дихальних шляхів із періодичними нападами." },
                new Disease { Name = "Пневмонія", Description = "Інфекційне ураження легеневої тканини, частіше бактеріального походження." },
                new Disease { Name = "Мігрень", Description = "Систематичні головні болі, часто з нудотою, світлобоязню та аурою." },
                new Disease { Name = "Артрит", Description = "Запальне ураження суглобів, що обмежує рухливість та спричиняє біль." },
                new Disease { Name = "Гастрит", Description = "Запалення слизової оболонки шлунка з болем та диспепсією." },
                new Disease { Name = "Отит", Description = "Запалення вуха, здебільшого середнього, часто спричинене інфекцією." },
                new Disease { Name = "Анемія", Description = "Зменшення рівня гемоглобіну та еритроцитів у крові, що викликає втому." },
                new Disease { Name = "Риніт", Description = "Запалення слизової носа, зазвичай при застуді або алергії." },
                new Disease { Name = "Псоріаз", Description = "Хронічне неінфекційне шкірне захворювання з лущенням та свербінням." },
                new Disease { Name = "Бронхіт", Description = "Запалення слизової оболонки бронхів, переважно з кашлем і мокротинням." },
                new Disease { Name = "Грип", Description = "Вірусне захворювання з високою температурою, ломотою та загальною слабкістю." },
                new Disease { Name = "Варикоз", Description = "Розширення вен нижніх кінцівок із порушенням венозного відтоку." },
                new Disease { Name = "Хронічний тонзиліт", Description = "Довготривале запалення мигдаликів, часто після частих ангін." },
                new Disease { Name = "Депресія", Description = "Порушення настрою, що характеризується стійкою пригніченістю та втратою інтересу." },
                new Disease { Name = "Карієс", Description = "Руйнування твердих тканин зуба." },
                new Disease { Name = "Кон'юнктивіт", Description = "Запалення кон'юнктиви ока." }
            };

            var visits = new List<Visit>();
            for (int i = 0; i < 15; i++)
            {
                visits.Add(new Visit { VisitDate = RandomDate(), Doctor = doctors[i], Patient = patients[i] });
            }
            visits.Add(new Visit { VisitDate = RandomDate(), Doctor = doctors[0], Patient = patients[1] });
            visits.Add(new Visit { VisitDate = RandomDate(), Doctor = doctors[2], Patient = patients[4] });
            visits.Add(new Visit { VisitDate = RandomDate(), Doctor = doctors[7], Patient = patients[6] });

            var diagnoses = new List<Diagnosis>();
            for (int i = 0; i < 15; i++)
            {
                diagnoses.Add(new Diagnosis { Description = $"Діагноз {i}", Disease = diseases[i], Visit = visits[i] });
            }
            diagnoses.Add(new Diagnosis { Description = "Пацієнт висловлює стійке зниження настрою та апатію.", Disease = diseases[15], Visit = visits[15] });
            diagnoses.Add(new Diagnosis { Description = "Виявлено початкові ознаки карієсу на кількох зубах.", Disease = diseases[16], Visit = visits[16] });
            diagnoses.Add(new Diagnosis { Description = "Почервоніння та сльозотеча очей. Діагностовано кон'юнктивіт.", Disease = diseases[17], Visit = visits[17] });

            await _context.Doctors.AddRangeAsync(doctors);
            await _context.SaveChangesAsync();
            await _context.Patients.AddRangeAsync(patients);
            await _context.SaveChangesAsync();
            await _context.Disease.AddRangeAsync(diseases);
            await _context.SaveChangesAsync();
            await _context.Visits.AddRangeAsync(visits);
            await _context.SaveChangesAsync();
            await _context.Medications.AddRangeAsync(medications);
            await _context.SaveChangesAsync();
            await _context.Diagnosiss.AddRangeAsync(diagnoses);
            await _context.SaveChangesAsync();

            return "Базу перезаповнено тестовими даними.";
        }

        static DateTime RandomDate()
        {
            var rnd = new Random();
            int daysAgo = rnd.Next(1, 365);
            return DateTime.Now.Date.AddDays(-daysAgo);
        }
    }
}
