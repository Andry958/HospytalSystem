﻿namespace HospitalSystem.Models.Dto
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
