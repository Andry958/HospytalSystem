﻿namespace HospitalSystem.Models.Dto
{
    public class MedicationRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
