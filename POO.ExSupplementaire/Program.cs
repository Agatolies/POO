using System;

namespace POO.ExSupplementaire
{
    class Program
    {
        static void Main(string[] args)
        {
            JourneeDeTravail jour1 = new JourneeDeTravail();

            jour1.AjouterPrestation("Faire tourner macro", 30, Prestation.TauxTvaDefaut);
            jour1.AjouterPrestation("Envoyer rappels", 90, Prestation.TauxTvaDefaut);
            jour1.AjouterPrestation("Annulations", 90, 21);
            jour1.AjouterPrestation("Glandouille", 120, Prestation.TauxTvaDefaut);
            jour1.Afficher();

            jour1.Fusionner(jour1[1].Numero, jour1[2].Numero);
            jour1.Fusionner(jour1[3].Numero, jour1[4].Numero);
            jour1.Afficher();

            bool retVal = jour1.AjouterPrestation("Ne pas décrocher à l'ours", 40, 21);
            if (!retVal)
                Console.WriteLine("ça a bien planté comme on s'y attendait\n");

            jour1.AjouterPrestation("Ne pas décrocher à l'ours", 20, 21);
            jour1.Afficher();

            Console.ReadLine();
        }
    }
}
