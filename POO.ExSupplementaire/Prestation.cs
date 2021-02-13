using System;
using System.Collections.Generic;
using System.Text;

namespace POO.ExSupplementaire
{
    public class Prestation
    {
        //-- STATIQUES (relatif à mon usine)
        //-- Propriétés statiques

        public static int ProchainNumero { get; set; }
        public static decimal PrixDefautPar15Minutes { get; set; }
        public static decimal TauxTvaDefaut { get; set; }

        //-- Constructeur statique

        static Prestation()
        {
            ProchainNumero = 1;
            PrixDefautPar15Minutes = 30;
            TauxTvaDefaut = 21.5M;
        }

        //-- INSTANCES (relatif à mes instances de classe)
        //-- Variables d'instance

        private int _numero;
        private string _description;
        private int _dureeEffective;
        private decimal _tauxTva;

        //-- Constructeurs d'instance

        public Prestation(string description, int duree, decimal tauxTva)
        {
            bool aDureePrestationNegative = duree < 0;
            if (aDureePrestationNegative)
                throw new Exception("La durée de la prestation ne peut être négative.");

            Description = description;
            DuréeEffective = duree;
            _tauxTva = tauxTva;
            _numero = Prestation.ProchainNumero;

            Prestation.ProchainNumero++;
        }

        public Prestation(string description, int duree)
            : this(description, duree, Prestation.TauxTvaDefaut)
        {
        }

        public Prestation(string description)
            : this(description, 30)
        { 
        }

        //-- Propriétés d'instance

        public int Numero 
        {
            get { return _numero; }        
        }

        public decimal TauxTva
        {
            get { return _tauxTva; }
        }

        public string Description
        {
            get { return _description; }
            private set 
            {
                bool estTropCourt = value.Length <= 5;
                if (estTropCourt)
                    throw new Exception("La valeur affectée doit contenir minimum 6 caractères.");

                _description = value.ToUpper();
            }
        }

        public int DuréeEffective
        {
            get { return _dureeEffective; }
            set 
            {
                bool estNegatifOuNull = value <= 0;
                if (estNegatifOuNull)
                    throw new Exception("La valeur doit être supérieure à 0.");

                _dureeEffective = value;
            }

        }

        public decimal PrixTTC
        {
            get 
            {
                bool estEntame = (DuréeEffective % 15) > 0;
                int nombreQuartHeureEntame = DuréeEffective / 15;
                if (estEntame)
                    nombreQuartHeureEntame++;

                decimal prixSansTva = nombreQuartHeureEntame * Prestation.PrixDefautPar15Minutes;
                decimal prixAvecTva = prixSansTva * TauxTva /100;

                return Math.Round(prixAvecTva, 2);
            }
        }

        //-- Méthodes d'instance

        public Prestation Fusionner(Prestation autre)
        {
            //string description = $"{NettoyerDescription(this.Description)} {NettoyerDescription(autre.Description)}";
            //string description = String.Format(
            //    "{0} {1}",
            //    NettoyerDescription(this.Description), 
            //    NettoyerDescription(autre.Description));
            string description = FormaterDescription(this.Description, autre.Description);

            int duree = this.DuréeEffective + autre.DuréeEffective;
            decimal tauxTva = Math.Min(this.TauxTva, autre.TauxTva);

            return new Prestation(description, duree, tauxTva);
        }

        private string NettoyerDescription(string description)
        {
            return description.ToUpper().Trim();
        }

        private string FormaterDescription(string description1, string description2)
            => String.Format(
                "{0} {1}",
                NettoyerDescription(description1),
                NettoyerDescription(description2));
    }
}
