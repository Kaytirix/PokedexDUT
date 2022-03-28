using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDex
{
        //Classe généré par un convertiseur Json vers une classe C#
        public class Name
        {
            public string en { get; set; }
            public string fr { get; set; }

        }
        public class Genus
        {
            public string en { get; set; }
            public string fr { get; set; }

        }
        public class Description
        {
            public string en { get; set; }
            public string fr { get; set; }

        }
        public class Stats
        {
            public string name { get; set; }
            public int stat { get; set; }

        }

        public class Pokemon
        {
            public int id { get; set; }
            public Name name { get; set; }
            public IList<string> types { get; set; }
            public int height { get; set; }
            public int weight { get; set; }
            public Genus genus { get; set; }
            public Description description { get; set; }
            public IList<Stats> stats { get; set; }
            public int lastEdit { get; set; }

            //Name : NumeroEtNomPokemon
            //Input : Tableau de pokémon -> MesPokemon
            //Output : Affichage de l'id du pokemon et de son nom en français
            //Valeur ajouté : Affiche le numéro et le nom de tous les pokemons
            public static void NumeroEtNomPokemon(Pokemon[] MesPokemon)
            {
                for (int index = 0; index < Constantes.NombrePokemon; index++)
                {
                    Console.WriteLine("Numéro du pokemon : {0}", MesPokemon[index].id);
                    Console.WriteLine("Nom du pokemon : {0}", MesPokemon[index].name.fr);
                    Console.WriteLine("------------------------");
                }
            }


            //Name : PokemonGen3
            //Input : Tableau de pokémon -> MesPokemon
            //Output : L'affichage du nom du pokemons
            //Valeur ajouté : Affiche le nom de tous les pokemons de la génération 3
            public static void PokemonGen3(Pokemon[] MesPokemon)
            {
                for (int index = Constantes.DebutGenTab[2] - 1; index < Constantes.FinGenTab[2]; index++)
                {
                    Console.WriteLine("Nom du pokemon : {0}", MesPokemon[index].name.fr);
                }
            }

            //Name : TypePokemonChaqueGen
            //Input : Tableau de pokémon -> MesPokemon
            //Output : L'affichage du ou des types du pokemon
            //Valeur ajouté : Affiche le ou les types de tous les pokemons selon chaque génération
            public static void TypePokemonChaqueGen(Pokemon[] MesPokemon)
            {
                //Liste temporaire des types déjà affiché
                List<string> tempType = new List<string>();
                
                //Pour chaque génération
                for (int index = 0; index < Constantes.DebutGenTab.Length; index++)
                {
                    Console.WriteLine("\nGénération {0}\n", index + 1);

                    //Important de vider la liste afin d'afficher tous les types mêmes s'ils ont déjà été afficher
                    tempType.Clear();

                    for (int i = Constantes.DebutGenTab[index]-1; i < Constantes.FinGenTab[index]; i++)
                    {
                        //Si le pokemon en question comporte un seul type
                        if(MesPokemon[i].types.Count == 1)
                        {
                            //Si le type du pokemon n'est pas déjà afficher
                            if(tempType.Contains(MesPokemon[i].types[0]) == false)
                            {
                                Console.WriteLine("{0} est de type : {1}", MesPokemon[i].name.fr, MesPokemon[i].types[0]);
                                tempType.Add(MesPokemon[i].types[0]); //On enregistre le type dans la liste des types déjà afficher
                            }
                        }
                        else //Si le pokemon comporte deux types
                        {
                            //Enregistre les deux types dans une seul variable, séparer par une virgule
                            string TypeDouble = MesPokemon[i].types[0] + "," + MesPokemon[i].types[1];

                            if (tempType.Contains(TypeDouble) == false)
                            {
                                Console.WriteLine("{0} et de type : {1}", MesPokemon[i].name.fr, TypeDouble);
                                tempType.Add(TypeDouble); //On enregistre le type dans la liste des types déjà afficher
                            }
                        }
                    }              
                }
            }

            //Name : TousTypePokemon
            //Input : Tableau de pokémon -> MesPokemon
            //Output : Affiche le nom de tous les pokemons selon le type de l'utilisateur
            //Valeur ajouté : Permet d'affichier une liste de tous les pokemons comportant le type que l'utilisateur recherche
            public static void TousTypePokemon(Pokemon[] MesPokemon, string TypePokemon)
            {
                Console.WriteLine("\nVoici tous les pokemons du type {0}\n", TypePokemon);

                //Pour chaque génération
                for (int index = 0; index < Constantes.DebutGenTab.Length; index++)
                {
                    for (int i = Constantes.DebutGenTab[index] - 1; i < Constantes.FinGenTab[index]; i++)
                    {
                        //Si le pokemon possède 1 seul type et qui soit égal au type rechercher
                        if (MesPokemon[i].types[0] == TypePokemon && MesPokemon[i].types.Count == 1)
                        {
                                Console.WriteLine("{0}", MesPokemon[i].name.fr);
                        }
                    }
                }
            }

            //Name : ListePokemonTypeAcier
            //Input : Tableau de pokémon -> MesPokemon
            //Output : Affiche la moyenne des poids de tous les pokemons Acier
            //Valeur ajouté : Permet d'afficher la moyenne des poids de tous les pokemons de type Acier
            public static void ListePokemonTypeAcier(Pokemon[] MesPokemon)
                {
                int PoidTypeAcier = 0;
                int nbPokemon = 0;

                Console.WriteLine("\nVoici tous les pokemons du type Acier\n");

                //Pour chaque génération
                for (int index = 0; index < Constantes.DebutGenTab.Length; index++)
                {
                    for (int i = Constantes.DebutGenTab[index] - 1; i < Constantes.FinGenTab[index]; i++)
                    {
                        //Si le pokemon possède 1 seul type et qui soit égal au type Acier
                        if (MesPokemon[i].types[0] == "Steel" && MesPokemon[i].types.Count == 1)
                        {
                            PoidTypeAcier += MesPokemon[i].weight;
                            nbPokemon++;
                        }
                    }
                }

                Console.WriteLine("La moyenne des poids de tous les types Acier {0}", PoidTypeAcier/nbPokemon);
            }
        }
}
