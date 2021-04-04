using System;

namespace ProjTravelUnitTest.Api.Models
{
    public class Client
    {
        #region Propriedades
            public int Id { get; set; }
            public string Name { get; set; }
            public string Telephone { get; set; }
            public DateTime BirthdayDate { get; set; }
            public int NumberOfChildren { get; set; }

        #endregion
    }
}