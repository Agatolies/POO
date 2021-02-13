using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO.ExSupplementaire
{
    public class JourneeDeTravail : IEnumerable
    {
        //-- Variables d'instance

        private List<Prestation> _prestations;

        //-- Constructeur

        public JourneeDeTravail()
        {
            _prestations = new List<Prestation>();
        }

        //-- Indexeur

        //public Prestation this[int numero]
        //{
        //    get { return _prestations.FirstOrDefault(prestation => prestation.Numero == numero); }
        //}

        public Prestation this[int numPrestation]
        {
            get
            {
                Prestation retVal = null;

                foreach (Prestation p in this)
                {
                    if (p.Numero == numPrestation)
                    {
                        retVal = p;
                        break;
                    }
                }

                return retVal;
            }
        }

        //-- Propriétés d'instance

        public decimal DureeTotale
        {
            get 
            {
                decimal dureeTotale = 0;
                foreach (var prestation in _prestations)
                    dureeTotale += prestation.DuréeEffective;

                return dureeTotale;
            }
        }
        
        //-- Méthodes

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_prestations).GetEnumerator();
        }

        public bool AjouterPrestation(string description, int duree, decimal tauxTva)
        {
            try
            {
                Prestation nouvellePrestation = new Prestation(description, duree, tauxTva);

                bool estSuperieurALimite = DureeTotale + duree > 360;
                if (estSuperieurALimite)
                    throw new Exception("La journée ne peut pas dépassé 6h.");

                _prestations.Add(nouvellePrestation);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool Fusionner(int noPrestation1, int noPrestation2)
        {
            try
            {
                Prestation prestation1 = this[noPrestation1];
                Prestation prestation2 = this[noPrestation2];

                if (prestation1 == null || prestation2 == null)
                    throw new Exception("Nous n'avons pas pu fusionner les 2 prestations.");

                Prestation nouvellePrestation = prestation1.Fusionner(prestation2);

                _prestations.Remove(prestation1);
                _prestations.Remove(prestation2);
                _prestations.Add(nouvellePrestation);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }

        public void Afficher()
        {
            foreach (Prestation courante in _prestations)
                Console.WriteLine($"{courante.Numero}. {courante.Description}");

            Console.WriteLine("Durée totale: " + DureeTotale);
            Console.WriteLine();
        }

    }
}
