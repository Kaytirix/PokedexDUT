using System;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;

namespace PokeDex
{
    class Program
    {
        static void Main(string[] args)
        {
            //Numéro de la tâche demandée
            int NumTache;

            //Tableau de pokemon
            Pokemon[] MesPokemon = new Pokemon[Constantes.NombrePokemon];

            Console.WriteLine("En attente du chargement du PokeDex");

            //Si tout les pokemons sont initialisés
            if (InitPokeDex(MesPokemon))
            {
                Console.WriteLine("PokeDex chargé !");
                //Boucle "pseudo" infini qui permet de toujours retourner sur le menu
                while (true)
                {
                    Console.WriteLine("\nVoici la liste des tâches que le Pokedex peut faire :\n");

                    Console.WriteLine("1 - Afficher la liste de tous les pokémons (numéro et nom)");
                    Console.WriteLine("2 - Afficher un Pokémons de chaque type pour chaque génération");
                    Console.WriteLine("3 - Afficher tous les Pokémons d’un type (au choix)");
                    Console.WriteLine("4 - Afficher tous les Pokémons de la génération 3");
                    Console.WriteLine("5 - Afficher la moyenne des poids des Pokémons de types Acier");
                    Console.WriteLine("0 - Pour quitter\n");

                    Console.Write("Taper le numéro de la tâche qui vous intéresse : ");

                    NumTache = Convert.ToInt32(Console.ReadLine());

                    //Si l'utilisateur tape un nombre qui n'est pas une tâche
                    if (NumTache < 0 || NumTache > 5)
                    {
                        Console.WriteLine("Vous avez séléctionné une tâche inexistante\n");
                    }
                    else
                    {
                        Menu(NumTache, MesPokemon);
                    }
                }
            }
            else
            {
                Console.WriteLine("Le chargement du PokeDex à rencontré une erreur\n");
            }
        }

        //Name : InitPokeDex
        //Input : Tableau de pokémon -> MesPokemon
        //Output : boolean 
        //Valeur ajouté : Initialise tous les pokemons 
        static bool InitPokeDex(Pokemon[] MesPokemon)
        {
            //Liste qui comportera tous les threads en attente de lancement
            List<Thread> ListeThread = new List<Thread>();

            //Boucle pour initialiser les threads selon les générations
            for(int index = 0; index < 8 ; index++)
            {
                int nbMin = Constantes.DebutGenTab[index];
                int nbMax = Constantes.FinGenTab[index];
                ListeThread.Add(new Thread(() => Generation(MesPokemon, nbMin, nbMax))); //On passe en paramètre le tableau de pokémon vide afin de bien initialiser les objets pokemons
            }

            //Lance tous les threads
            foreach (Thread LeThread in ListeThread)
                LeThread.Start();
            
            
            while (ListeThread.Count != 0)
            {
                for(int index = 0; index < ListeThread.Count ; index++)
                {
                    if (ListeThread[index].IsAlive) //Vérifie constament si le thread sélectionner dans la liste est toujours actif
                        Thread.Sleep(500); //si oui on attend 500ms afin d'éviter un ping vers le threads toute les 5ms
                    else
                        ListeThread.RemoveAt(index); //si il n'est plus actif, on l'enlève de la liste des threads afin d'éviter de revérifier s'il est actif
                }
            }

            //Si tout est initialiser, on retourne vrai
            return true;
        }

        //Name : Generation
        //Input : Tableau de pokémon -> MesPokemon, le début de la génération -> DebutGen, la fin de la génération -> FinGen
        //Output : Le tableau de pokémon
        //Valeur ajouté : Attribu à tous les pokémons les informations lui concernant en convertissant les données de l'API
        static Pokemon[] Generation(Pokemon[] MesPokemon, int DebutGen, int FinGen)
        {
            for (int position = DebutGen; position <= FinGen; position++)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    string jsonString = webClient.DownloadString("https://tmare.ndelpech.fr/tps/pokemons/" + position);
                    MesPokemon[position - 1] = JsonSerializer.Deserialize<Pokemon>(jsonString);
                }
            }
            return MesPokemon;
        }

        //Name : Menu
        //Input : Le numéro de la tâche demander -> NumTache, Tableau de pokémon -> MesPokemon
        //Output : xxx
        //Valeur ajouté : Permet d'appeler les fonctions liées à la demande de l'utilisateur
        static void Menu(int NumTache, Pokemon[] MesPokemon)
        {
            switch (NumTache)
            {
                case 0:
                    Environment.Exit(0);break; //Quitte le programme
                case 1:
                    Pokemon.NumeroEtNomPokemon(MesPokemon); break;
                case 2:
                    Pokemon.TypePokemonChaqueGen(MesPokemon); break;
                case 3:
                    Console.WriteLine("Quel est le type que vous cherchez ?");Pokemon.TousTypePokemon(MesPokemon, Console.ReadLine());break;
                case 4:
                    Pokemon.PokemonGen3(MesPokemon); break;
                case 5:
                    Pokemon.ListePokemonTypeAcier(MesPokemon);break;
            }
        }
    }
}
