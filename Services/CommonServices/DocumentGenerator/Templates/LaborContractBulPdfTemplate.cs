using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator.Templates
{
    public class LaborContractBulPdfTemplate : ITemplate
    {
        public LaborContractBulPdfTemplate( )
        {
            BuildTemplate( );
        }

        public Document Document { get; private set; }

        public IDictionary<string, Dictionary<string, Paragraph>> SectionsDictionary { get; set; }

        //########################################################

        private void BuildTemplate( )
        {
            Dictionary<string, Paragraph> paragraphs = new Dictionary<string, Paragraph>( );

            this.SectionsDictionary = new Dictionary<string, Dictionary<string, Paragraph>>
            {
                {"laborMainSection",paragraphs },
            };

            this.Document = new Document( )
            {
                Info =
                {
                    Title = "Labor Contract",
                    Subject = "Template of Labor contract",
                    Author = "Nik",
                }
            };

            this.DocumentStyle( this.Document );

            Section? sectionMain = this.Document.AddSection( );

            this.DocumentPageSetup( sectionMain );

            this.TopSection( this.Document, paragraphs );

            this.CompanySection( this.Document, paragraphs );

            this.EmployeeSectiom( this.Document, paragraphs );

            this.ContractBodyTartOne( this.Document, paragraphs );

            this.ContractBodyTartTwo( this.Document, paragraphs );

            this.ContractBodyTartThree( this.Document, paragraphs );

            this.ContractBodyTartFoutToNine( this.Document, paragraphs );

            this.SignatureSection( this.Document, paragraphs );
        }

        private void DocumentPageSetup( Section? mainSection )
        {
            mainSection.PageSetup.PageFormat = PageFormat.A4;
            mainSection.PageSetup.Orientation = Orientation.Portrait;
            mainSection.PageSetup.TopMargin = Unit.FromMillimeter( 10 );
            mainSection.PageSetup.RightMargin = Unit.FromMillimeter( 15 );
            mainSection.PageSetup.LeftMargin = Unit.FromMillimeter( 15 );
            mainSection.PageSetup.BottomMargin = Unit.FromMillimeter( 10 );
        }

        private void DocumentStyle( Document? document )
        {
            Style? docStyle = document.Styles[StyleNames.Normal];
            docStyle.Font.Size = Unit.FromPoint( 9 );
            docStyle.Font.Name = "Arial";
            document.DefaultTabStop = Unit.FromMillimeter( 7 );
        }

        private void TopSection( Document? document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraContract = document.LastSection.AddParagraph( );
            paraContract.Add( this.BuildFormattedText( TextBulPdfTemplate.ContractText ) );
            paraContract.Format.Font.Size = Unit.FromPoint( 15 );
            paraContract.Format.Alignment = ParagraphAlignment.Center;
            paraContract.Format.SpaceAfter = Unit.FromMillimeter( 4 );

            Paragraph? paraContractNumber = document.LastSection.AddParagraph( TextBulPdfTemplate.ContractNumber );
            paraContractNumber.Format.Alignment = ParagraphAlignment.Center;
            paraContractNumber.Format.SpaceAfter = Unit.FromMillimeter( 4 );

            Paragraph? paraRegPlace = document.LastSection
                                              .AddParagraph( TextBulPdfTemplate.DocumentDateText );
            paraRegPlace.Format.Alignment = ParagraphAlignment.Left;
            paraRegPlace.Format.SpaceAfter = Unit.FromMillimeter( 5 );

            paragraphs.Add( "laborContract", paraContract );
            paragraphs.Add( "contractNumber", paraContractNumber );
            paragraphs.Add( "registationPlace", paraRegPlace );
        }

        private void CompanySection( Document? document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraCompanyName = document.LastSection
                                                 .AddParagraph( TextBulPdfTemplate.CompanyName );
            paraCompanyName.Format.Alignment = ParagraphAlignment.Center;

            Paragraph? paraNote = document.LastSection
                                          .AddParagraph( TextBulPdfTemplate.NoteCompanyText );
            paraNote.Format.Alignment = ParagraphAlignment.Center;
            paraNote.Format.Font.Size = Unit.FromPoint( 6 );

            Paragraph? paraCompanyInfo = document.LastSection.AddParagraph( );

            var boldManagerText = this.BuildFormattedText( TextBulPdfTemplate.ManagerStrPartOne );

            paraCompanyInfo.AddText( TextBulPdfTemplate.Headquarters + TextBulPdfTemplate.BulstatManagerStr );
            paraCompanyInfo.Add( boldManagerText );
            paraCompanyInfo.AddText( TextBulPdfTemplate.ManagerStrPartTwo );

            paraCompanyInfo.Format.Alignment = ParagraphAlignment.Justify;
            paraCompanyInfo.Format.SpaceBefore = Unit.FromMillimeter( 4 );
            paraCompanyInfo.Format.SpaceAfter = Unit.FromMillimeter( 4 );

            paragraphs.Add( "companyName", paraCompanyName );
            paragraphs.Add( "companyNameNote", paraNote );
            paragraphs.Add( "companyInformation", paraCompanyInfo );
        }

        private void EmployeeSectiom( Document? document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraEmpFullName = document.LastSection
                                                 .AddParagraph( TextBulPdfTemplate.EmpFullName );
            paraEmpFullName.Format.Alignment = ParagraphAlignment.Center;

            Paragraph? paraEmpNameNote = document.LastSection
                                                 .AddParagraph( TextBulPdfTemplate.NoteEmpNameText );
            paraEmpNameNote.Format.Alignment = ParagraphAlignment.Center;
            paraEmpNameNote.Format.Font.Size = Unit.FromPoint( 6 );


            Paragraph? paraNoteEmployee = document.LastSection
                                                  .AddParagraph( TextBulPdfTemplate.NoteEmployee );

            paraNoteEmployee.Add( this.BuildFormattedText( TextBulPdfTemplate.NoteEmployeePartTwo ) );
            paraNoteEmployee.AddText( TextBulPdfTemplate.NoteEmployeePartThree );
            paraNoteEmployee.Format.SpaceBefore = Unit.FromMillimeter( 4 );

            string empParaText = TextBulPdfTemplate.EmpCivilNumber
                               + TextBulPdfTemplate.EmpPermanentAddress
                               + TextBulPdfTemplate.EmpIdDocument
                               + TextBulPdfTemplate.EmpEducation
                               + TextBulPdfTemplate.EmpDiploma
                               + TextBulPdfTemplate.EmpWorkExperience;

            Paragraph? paraEmpInformation = document.LastSection.AddParagraph( empParaText );
            paraEmpInformation.Format.Alignment = ParagraphAlignment.Justify;

            paragraphs.Add( "employeeName", paraEmpFullName );
            //paragraphs.Add("employeeNameNote", paraEmpNameNote);
            paragraphs.Add( "employeeInformation", paraEmpInformation );
        }

        private void ContractBodyTartOne( Document? document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraContractReason = document.LastSection
                                                    .AddParagraph( TextBulPdfTemplate.ContractReason );
            paraContractReason.Format.SpaceAfter = Unit.FromMillimeter( 3 );

            Paragraph? paraPartOne = document.LastSection.AddParagraph( TextBulPdfTemplate.PartOne );

            Paragraph? paraPartOnePointOne = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartOnePointOne );

            paraPartOnePointOne.Format.LeftIndent = document.DefaultTabStop;
            paraPartOnePointOne.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartOneDepartment = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartOnePointDepartment );

            paraPartOneDepartment.Format.LeftIndent = document.DefaultTabStop;
            paraPartOneDepartment.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartOnePointTwo = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartOnePointTwo );
            paraPartOnePointTwo.Format.LeftIndent = document.DefaultTabStop;
            paraPartOnePointTwo.Format.Alignment = ParagraphAlignment.Justify;

            paragraphs.Add( "contractReason", paraContractReason );
            paragraphs.Add( "contractOne-One", paraPartOnePointOne );
            paragraphs.Add( "contractOneDepartment", paraPartOneDepartment );
            paragraphs.Add( "contractOne-Two", paraPartOnePointTwo );

        }

        private void ContractBodyTartTwo( Document document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraPartTwo = document.LastSection.AddParagraph( TextBulPdfTemplate.PartTwo );

            Paragraph? paraPartTwoPointOne = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartTwoPointOne );

            paraPartTwoPointOne.Format.LeftIndent = document.DefaultTabStop;
            paraPartTwoPointOne.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartTwoPointTwo = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartTwoPointTwo );

            paraPartTwoPointTwo.Format.LeftIndent = document.DefaultTabStop;
            paraPartTwoPointTwo.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartTwoPointThree = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartTwoPointThree );

            paraPartTwoPointThree.Format.LeftIndent = document.DefaultTabStop;
            paraPartTwoPointThree.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartTwoPointFour = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartTwoPointFour );

            paraPartTwoPointFour.Format.LeftIndent = document.DefaultTabStop;
            paraPartTwoPointFour.Format.Alignment = ParagraphAlignment.Justify;
        }

        private void ContractBodyTartThree( Document document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraPartThree = document.LastSection.AddParagraph( TextBulPdfTemplate.PartThree );

            Paragraph? paraPartThreePointOne = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartThreePointOne );

            paraPartThreePointOne.Format.LeftIndent = document.DefaultTabStop;
            paraPartThreePointOne.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartThreePointTwo = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartThreePointTwo );

            paraPartThreePointTwo.Format.LeftIndent = document.DefaultTabStop;
            paraPartThreePointTwo.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartThreePointThree = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartThreePointThree );

            paraPartThreePointThree.Format.LeftIndent = document.DefaultTabStop;
            paraPartThreePointThree.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartThreePointFour = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartThreePointFour );

            paraPartThreePointFour.Format.LeftIndent = document.DefaultTabStop;
            paraPartThreePointFour.Format.Alignment = ParagraphAlignment.Justify;
        }

        private void ContractBodyTartFoutToNine( Document document, Dictionary<string, Paragraph> paragraphs )
        {
            Paragraph? paraPartFour = document.LastSection.AddParagraph( TextBulPdfTemplate.PartFour );

            Paragraph? paraPartFourPointOne = document.LastSection
                                                     .AddParagraph( TextBulPdfTemplate.PartFourPartOne );

            paraPartFourPointOne.Format.LeftIndent = document.DefaultTabStop;
            paraPartFourPointOne.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartFourPointTwo = document.LastSection
                                                      .AddParagraph( TextBulPdfTemplate.PartFourPartTwo );

            paraPartFourPointTwo.Format.LeftIndent = document.DefaultTabStop;
            paraPartFourPointTwo.Format.Alignment = ParagraphAlignment.Justify;

            Paragraph? paraPartFive = document.LastSection.AddParagraph( TextBulPdfTemplate.PartFive );

            Paragraph? paraPartSix = document.LastSection.AddParagraph( TextBulPdfTemplate.PartSix );

            Paragraph? paraPartSeven = document.LastSection.AddParagraph( TextBulPdfTemplate.PartSeven );

            Paragraph? paraPartSevenPartOne = document.LastSection
                                                      .AddParagraph( TextBulPdfTemplate.PartSevenPointOne );
            paraPartSevenPartOne.Format.LeftIndent = document.DefaultTabStop;

            Paragraph? paraPartEight = document.LastSection.AddParagraph( TextBulPdfTemplate.PartEight );

            Paragraph? paraAfterEight = document.LastSection.AddParagraph( TextBulPdfTemplate.AfterEight );
            paraAfterEight.Format.SpaceBefore = Unit.FromMillimeter( 1 );

            paragraphs.Add( "basicLeave", paraPartFourPointOne );
            paragraphs.Add( "additionalLeave", paraPartFourPointTwo );
            paragraphs.Add( "contractTerm", paraPartFive );
            paragraphs.Add( "noticePeriod", paraPartSix );
            paragraphs.Add( "admissionDate", paraPartEight );
        }

        private void SignatureSection( Document document, Dictionary<string, Paragraph> paragraphs )
        {
            AddManagerEmpSignatureParagraph( document );

            //#################################################################################
            Paragraph? paraPartNine = document.LastSection.AddParagraph( TextBulPdfTemplate.PartNine );
            paraPartNine.Format.SpaceBefore = Unit.FromMillimeter( 3 );

            Paragraph? paraPartNinePointOne = document.LastSection
                                                      .AddParagraph( TextBulPdfTemplate.PartNinePointOne );
            paraPartNinePointOne.Format.LeftIndent = document.DefaultTabStop;

            //###################################################################################

            AddHrEmpSignatureParagraph( document );

            Paragraph? paraRealDateOfAdmission = document.LastSection
                                                         .AddParagraph( TextBulPdfTemplate.RealDateOfAdmission );
            paraRealDateOfAdmission.Format.LeftIndent = document.DefaultTabStop;
            paraRealDateOfAdmission.Format.Font.Bold = true;
            paraRealDateOfAdmission.Format.SpaceBefore = Unit.FromMillimeter( 5 );

            AddHrEmpSignatureParagraph( document );

            paragraphs.Add( "paraPartNine", paraPartNine );
            paragraphs.Add( "realDateOfAdmission", paraRealDateOfAdmission );
        }

        private void AddManagerEmpSignatureParagraph( Document document )
        {
            Paragraph? paraEmpManagerSign = document.LastSection.AddParagraph( );
            paraEmpManagerSign.Add( BuildFormattedText( TextBulPdfTemplate.ManagerSignature ) );
            paraEmpManagerSign.AddText( TextBulPdfTemplate.SignatureDotes );
            paraEmpManagerSign.AddText( "\t\t\t\t\t\t" );
            paraEmpManagerSign.Add( BuildFormattedText( TextBulPdfTemplate.EmpSignature ) );
            paraEmpManagerSign.AddText( TextBulPdfTemplate.SignatureDotes );

            paraEmpManagerSign.Format.LeftIndent = document.DefaultTabStop;
            paraEmpManagerSign.Format.SpaceBefore = Unit.FromMillimeter( 5 );


            string textSignature = TextBulPdfTemplate.SignatureAndSeal
            + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + TextBulPdfTemplate.Signature;

            Paragraph? paraSign = document.LastSection.AddParagraph( textSignature );
            paraSign.Format.LeftIndent = document.DefaultTabStop * 5;
            paraSign.Format.Font.Size = Unit.FromPoint( 5 );
        }

        private void AddHrEmpSignatureParagraph( Document document )
        {
            Paragraph? paraHrEmpSign = document.LastSection.AddParagraph( );
            paraHrEmpSign.Add( BuildFormattedText( TextBulPdfTemplate.HRDepartment ) );
            paraHrEmpSign.AddText( TextBulPdfTemplate.SignatureDotes );
            paraHrEmpSign.AddText( "\t\t\t\t" );
            paraHrEmpSign.Add( BuildFormattedText( TextBulPdfTemplate.EmpSignature ) );
            paraHrEmpSign.AddText( TextBulPdfTemplate.SignatureDotes );


            paraHrEmpSign.Format.LeftIndent = document.DefaultTabStop;
            paraHrEmpSign.Format.SpaceBefore = Unit.FromMillimeter( 5 );

            string textSignatureHr = TextBulPdfTemplate.Signature
            + "\t\t\t\t\t\t\t\t\t\t\t\t" + TextBulPdfTemplate.Signature;

            Paragraph? paraSignHr = document.LastSection.AddParagraph( textSignatureHr );
            paraSignHr.Format.LeftIndent = document.DefaultTabStop * 8;
            paraSignHr.Format.Font.Size = Unit.FromPoint( 5 );
        }

        private FormattedText BuildFormattedText( string text )
        {
            FormattedText fText = new FormattedText
            {
                Bold = true,
            };

            fText.AddText( text );

            return fText;
        }

    }
}

