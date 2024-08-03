﻿namespace Final_project_PresentationLayer.Models.Profile_Update_View_Model
{
    public class FamilyHeadViewModel
    {
        public string? City { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }
        public ICollection<FamilyMemberModel> FamilyMembers { get; set; }
        public string? ProfileImage { get; set; }
        public string? Address { get; set; }
    }
    public class FamilyMemberModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Nin { get; set; }
        public Guid FamilyHeadId { get; set; }
    }

}
