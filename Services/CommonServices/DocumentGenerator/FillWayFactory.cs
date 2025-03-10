﻿
using LegalFramework.Services.DocumentGenerator.FillWayTemplates;

namespace LegalFramework.Services.DocumentGenerator
{
    public class FillWayFactory : IFillWayStore
    {
        public FillWayFactory( )
        {
            this.FillWayStore = new Dictionary<string, IFill>( )
                                {
                                    { "DefaultPdfDocument" , new FillDefaultPdf()},
                                    { "laborContract-bul-pdf" , new FillLaborContractBulPdf()},
                                };
        }

        public IDictionary<string, IFill> FillWayStore { get; set; }
    }
}
