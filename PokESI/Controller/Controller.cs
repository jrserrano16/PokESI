using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Xml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;


namespace PokESI
{
    public static class Controller
    {
        public static string Htoken = "lCDUb5Ol6Xos1uRifLp-DFTlgbuXjyMDn73PSETQXhc";
        private static Pokemon p = new Pokemon();

        public static void cargarPokedex(GridView p, List<Pokemon> pokemonList, List<Pokemon> pokemonListAUX)
        {

            // Cargar contenido de prueba
            XmlDocument docSol = new XmlDocument();
            docSol.Load("Pokemons.xml");
            XmlNode root = docSol.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Pokemons");
            foreach (XmlNode node in pokemonsRoot.ChildNodes)
            {
                Pokemon newPokemon = new Pokemon();
                newPokemon.name = node.Attributes["Name"].Value;
                newPokemon.id = Convert.ToInt32(node.Attributes["ID"].Value);
                newPokemon.specie = node.Attributes["Specie"].Value;
                newPokemon.height = node.Attributes["Height"].Value;
                newPokemon.weight = node.Attributes["Weight"].Value;
                newPokemon.starter = Convert.ToBoolean(node.Attributes["Starter"].Value);
                newPokemon.legendary = Convert.ToBoolean(node.Attributes["Legendary"].Value);
                newPokemon.mega = Convert.ToBoolean(node.Attributes["Mega"].Value);
                newPokemon.mythical = Convert.ToBoolean(node.Attributes["Mythical"].Value);
                newPokemon.ultraBeast = Convert.ToBoolean(node.Attributes["UltraBeast"].Value);
                newPokemon.generation = Convert.ToInt32(node.Attributes["Generation"].Value);
                newPokemon.image = node.Attributes["Image"].Value;
                newPokemon.description = node.Attributes["Description"].Value;
                newPokemon.health = Convert.ToDouble(node.Attributes["Health"].Value);
                newPokemon.level = Convert.ToInt32(node.Attributes["Level"].Value);
                newPokemon.combat = Convert.ToBoolean(node.Attributes["Combat"].Value);
                newPokemon.captured = Convert.ToBoolean(node.Attributes["Captured"].Value);
                newPokemon.experience = Convert.ToInt32(node.Attributes["Experience"].Value);

                newPokemon.types = new List<string>();
                newPokemon.abilities = new List<string>();
                newPokemon.evolutionLine = new List<string>();

                string[] words = node.Attributes["Type"].Value.Split(',');

                foreach (string t in words)
                {
                    newPokemon.types.Add(t);
                }

                words = node.Attributes["Evolution"].Value.Split(',');
                foreach (string e in words)
                {
                    newPokemon.evolutionLine.Add(e);
                }
                words = node.Attributes["Abilities"].Value.Split(',');
                foreach (string e in words)
                {
                    newPokemon.abilities.Add(e);
                }
                pokemonList.Add(newPokemon);
                pokemonListAUX.Add(newPokemon);

                PlantillaPokedex poke = new PlantillaPokedex(newPokemon);

                p.Items.Add(poke);
            }
        }





        public static void atacar(Ataque ataque, Pokemon p1, Pokemon p2, TextBlock tb, ProgressBar pb2, ProgressBar pb1, TextBox n, Storyboard s, UserControl uc, Grid g)
        {
            double potenciaLVL = getPotenciaNivel(ataque.power, p1);
            double potencia = getPotenciaType(ataque, p2);
            double dañoEnemigo = potencia*potenciaLVL;
            tb.Text = pb2.Value-dañoEnemigo +" / 100";
            pb2.Value -= dañoEnemigo;
            n.Text = p1.name.ToUpper() +" used "+ ataque.name.ToUpper() +" and infringed "+dañoEnemigo +" damage points on "+p2.name.ToUpper();
            if (pb2.Value <=0)
            {
                s = (Storyboard)uc.Resources["Pkn_Debilitado"];
                s.Begin();
                g.Visibility = Visibility.Collapsed;
                n.Text = p2.name.ToUpper() +" has been defeated by "+p1.name.ToUpper();
                showNotification(p1, "Fin del combate", "Ganador: "+p1.name);
                actualizarPokemon(25, p1, pb1);
                actualizarPokemon(10, p2, pb2);


            }
        }
        public static void actualizarPokemon(int expGanada, Pokemon po)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("Pokemons.xml");
            XmlNode root = doc.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Pokemons");


            po.experience+=expGanada;
            while (po.experience >=100)
            {
                po.experience-=100;
                ++po.level;
                string subir = po.name+" acaba de alcanzar el nivel "+po.level;
                showNotification(po, "Felicidades!!!", subir);
            }
            foreach (XmlNode node in pokemonsRoot.ChildNodes)
            {
                if (Convert.ToInt32(node.Attributes["ID"].Value)==po.id)
                {
                    node.Attributes["Level"].Value = po.level.ToString();
                    node.Attributes["Experience"].Value = po.experience.ToString();
                    break;
                }
            }
            doc.Save("Pokemons.xml");
        }
        public static void actualizarPokemon(Pokemon po1)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("Pokemons.xml");
            XmlNode root = doc.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Pokemons");
            foreach (XmlNode node in pokemonsRoot.ChildNodes)
            {
                if (Convert.ToInt32(node.Attributes["ID"].Value)==po1.id)
                {
                    node.Attributes["Health"].Value = po1.health.ToString();
                    break;
                }
            }
            doc.Save("Pokemons.xml");
        }
        public static void actualizarPokemon(int expGanada, Pokemon po, ProgressBar pb)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("Pokemons.xml");
            XmlNode root = doc.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Pokemons");


            po.experience+=expGanada;
            if (po.experience >= 100)
            {
                po.experience-=100;
                ++po.level;
                string subir = po.name+" acaba de alcanzar el nivel "+po.level;
                showNotification(po, "Felicidades!!!", subir);
            }

            foreach (XmlNode node in pokemonsRoot.ChildNodes)
            {
                if (Convert.ToInt32(node.Attributes["ID"].Value)==po.id)
                {
                    node.Attributes["Level"].Value = po.level.ToString();
                    node.Attributes["Health"].Value = pb.Value.ToString();
                    node.Attributes["Experience"].Value = po.experience.ToString();
                    break;
                }
            }
            doc.Save("Pokemons.xml");
        }
        public static void pokemonCapturado(Pokemon po)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("Pokemons.xml");
            XmlNode root = doc.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Pokemons");

            string subir = po.name+" ha sido atrapado ";
            showNotification(po, "Felicidades!!!", subir);

            int nuevonivel = po.level+1;

            subir = po.name+" acaba de alcanzar el nivel "+nuevonivel;
            showNotification(po, "Felicidades!!!", subir);

            foreach (XmlNode node in pokemonsRoot.ChildNodes)
            {
                if (Convert.ToInt32(node.Attributes["ID"].Value)==po.id)
                {

                    node.Attributes["Level"].Value = nuevonivel.ToString();
                    break;
                }
            }
            doc.Save("Pokemons.xml");
        }

        public static double getPotenciaNivel(double ot, Pokemon pk)
        {
            int l = pk.level;
            double pt = ot;
            if (l >= 35 && l < 50)
            {
                pt += 2;
            }
            else if (l >= 50 && l < 65)
            {
                pt += 4;
            }
            else if (l >= 80 && l < 90)
            {
                pt += 6;
            }
            else if (l >= 90 && l <= 100)
            {
                pt += 8;
            }
            return pt;
        }
        public static void showNotification(Pokemon p, string t1, string t2)
        {

            new ToastContentBuilder()
                 .AddArgument("action", "Favoritos")
                 .AddArgument("conversationId", 9813)
                 .AddCustomTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc))
                 .AddText(t1)
                 .AddText(t2)
                 .AddInlineImage(new Uri(p.image))
                 .AddAppLogoOverride(new Uri("ms-appx:///Assets/descubir.png"), ToastGenericAppLogoCrop.Default)
            .Show();
        }


        public static double getPotenciaType(Ataque atacante, Pokemon defensor)
        {
            double potencia = 1;
            if (atacante.type == "Normal")
            {
                if (defensor.types[0] == "Rock")
                {
                    potencia = 0.5;
                }
                else if (defensor.types[0] == "Ghost")
                {
                    potencia = 0;
                }
            }
            if (atacante.type == "Fight")
            {
                if (defensor.types[0] == "Normal" || defensor.types[0] == "Rock" || defensor.types[0] == "Ice")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Flying" || defensor.types[0] == "Poison" || defensor.types[0] == "Bug" || defensor.types[0] == "Psychic")
                {
                    potencia = 0.5;
                }
                else if (defensor.types[0] == "Ghost")
                {
                    potencia = 0;
                }

            }
            if (atacante.type == "Flying")
            {
                if (defensor.types[0] == "Fight" || defensor.types[0] == "Bug" || defensor.types[0] == "Grass")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Rock" || defensor.types[0] == "Electric")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Poison")
            {
                if (defensor.types[0] == "Bug" || defensor.types[0] == "Grass")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Poison" || defensor.types[0] == "Ground" || defensor.types[0] == "Rock" || defensor.types[0] == "Ghost")
                {
                    potencia = 0.5;
                }


            }
            if (atacante.type == "Ground")
            {
                if (defensor.types[0] == "Flying")
                {
                    potencia = 0;
                }
                else if (defensor.types[0] == "Poison" || defensor.types[0] == "Rock" || defensor.types[0] == "Fire" || defensor.types[0] == "Electric")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Bug" || defensor.types[0] == "Grass")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Rock")
            {
                if (defensor.types[0] == "Fight" || defensor.types[0] == "Ground")
                {
                    potencia = 0.5;
                }
                else if (defensor.types[0] == "Flying" || defensor.types[0] == "Bug" || defensor.types[0] == "Fire" || defensor.types[0] == "Ice")
                {
                    potencia = 2;
                }


            }
            if (atacante.type == "Bug")
            {
                if (defensor.types[0] == "Fight" || defensor.types[0] == "Flying" || defensor.types[0] == "Fire")
                {
                    potencia = 0.5;
                }
                else if (defensor.types[0] == "Poison" || defensor.types[0] == "Grass" || defensor.types[0] == "Psychic")
                {
                    potencia = 2;
                }

            }
            if (atacante.type == "Ghost")
            {
                if (defensor.types[0] == "Ghost")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Normal" || defensor.types[0] == "Psychic")
                {
                    potencia = 0;
                }


            }
            if (atacante.type == "Fire")
            {
                if (defensor.types[0] == "Bug" || defensor.types[0] == "Grass" || defensor.types[0] == "Ice")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Rock" || defensor.types[0] == "Fire" || defensor.types[0] == "Water" || defensor.types[0] == "Dragon")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Water")
            {
                if (defensor.types[0] == "Ground" || defensor.types[0] == "Rock" || defensor.types[0] == "Fire")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Water" || defensor.types[0] == "Grass" || defensor.types[0] == "Dragon")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Grass")
            {
                if (defensor.types[0] == "Ground" || defensor.types[0] == "Rock" || defensor.types[0] == "Water")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Flying" || defensor.types[0] == "Poison" || defensor.types[0] == "Bug" || defensor.types[0] == "Fire" || defensor.types[0] == "Grass" || defensor.types[0] == "Dragon")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Electric")
            {
                if (defensor.types[0] == "Flying" || defensor.types[0] == "Water")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Grass" || defensor.types[0] == "Electric" || defensor.types[0] == "Dragon")
                {
                    potencia = 0.5;
                }
                else if (defensor.types[0] == "Ground")
                {
                    potencia = 0;
                }

            }
            if (atacante.type == "Psychic")
            {
                if (defensor.types[0] == "Fight" || defensor.types[0] == "Poison")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Psychic")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Ice")
            {
                if (defensor.types[0] == "Flying" || defensor.types[0] == "Ground" || defensor.types[0] == "Grass" || defensor.types[0] == "Dragon")
                {
                    potencia = 2;
                }
                else if (defensor.types[0] == "Water" || defensor.types[0] == "Ice")
                {
                    potencia = 0.5;
                }

            }
            if (atacante.type == "Dragon")
            {
                if (defensor.types[0] == "Dragon")
                {
                    potencia = 2;
                }

            }
            return potencia;
        }

        /*
        public static void addPokemon()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Pokemons.xml");

            XmlNode root = doc.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Pokemons");
            XmlElement pokemonNode = doc.CreateElement("Pokemon");

            XmlAttribute idp = doc.CreateAttribute("ID");
            idp.Value = p.id.ToString();
            pokemonNode.Attributes.Append(idp);

            XmlAttribute nombre = doc.CreateAttribute("Name");
            nombre.Value = p.name;
            pokemonNode.Attributes.Append(nombre);

            XmlAttribute specie = doc.CreateAttribute("Specie");
            specie.Value = p.specie;
            pokemonNode.Attributes.Append(specie);


            for (int i = 0; i < p.types.Count; i++)
            {
                XmlAttribute types = doc.CreateAttribute("Type_"+(i+1));
                types.Value = p.types[i];
                pokemonNode.Attributes.Append(types);
            }

            

            XmlAttribute height = doc.CreateAttribute("Height");
            height.Value = p.height;
            pokemonNode.Attributes.Append(height);

            XmlAttribute weight = doc.CreateAttribute("Weight");
            weight.Value = p.weight;
            pokemonNode.Attributes.Append(weight);

            for (int i = 0; i < p.evolutionLine.Count; i++)
            {
                XmlAttribute evolution = doc.CreateAttribute("Evolution_"+(i+1));
                evolution.Value = p.evolutionLine[i];
                pokemonNode.Attributes.Append(evolution);
            }


            XmlAttribute starter = doc.CreateAttribute("Starter");
            starter.Value = p.starter.ToString();
            pokemonNode.Attributes.Append(starter);

            XmlAttribute legendary = doc.CreateAttribute("Legendary");
            legendary.Value = p.legendary.ToString();
            pokemonNode.Attributes.Append(legendary);

            XmlAttribute mythical = doc.CreateAttribute("Mythical");
            mythical.Value = p.mythical.ToString();
            pokemonNode.Attributes.Append(mythical);

            XmlAttribute mega = doc.CreateAttribute("Mega");
            mega.Value = p.mega.ToString();
            pokemonNode.Attributes.Append(mega);

            XmlAttribute ultraBeast = doc.CreateAttribute("UltraBeast");
            ultraBeast.Value = p.ultraBeast.ToString();
            pokemonNode.Attributes.Append(ultraBeast);

            XmlAttribute generation = doc.CreateAttribute("Generation");
            generation.Value = p.generation.ToString();
            pokemonNode.Attributes.Append(generation);


            XmlAttribute image = doc.CreateAttribute("Image");
            image.Value = p.image;
            pokemonNode.Attributes.Append(image);

            XmlAttribute description = doc.CreateAttribute("Description");
            description.Value = p.description.ToString();
            pokemonNode.Attributes.Append(description);

            XmlAttribute captured = doc.CreateAttribute("Captured");
            captured.Value = p.captured.ToString();
            pokemonNode.Attributes.Append(captured);

            XmlAttribute level = doc.CreateAttribute("Level");
            level.Value = p.level.ToString();
            pokemonNode.Attributes.Append(level);

            XmlAttribute experience = doc.CreateAttribute("Experience");
            experience.Value = p.experience.ToString();
            pokemonNode.Attributes.Append(experience);

        pokemonsRoot.AppendChild(pokemonNode);
            

            doc.Save("Pokemons.xml");
        }

        public static void newPokemon(IRestResponse response)
        {
           
            var pokemonA1 = JsonConvert.DeserializeObject<List<PokemonSearch>>(response.Content);
     

            p.name = pokemonA1[0].Name;
            p.id =  (int)pokemonA1[0].Number;
            p.specie = pokemonA1[0].Species;
            p.height = pokemonA1[0].Height;
            p.weight = pokemonA1[0].Weight;
            p.legendary = pokemonA1[0].Legendary;
            p.starter = pokemonA1[0].Starter;
            p.mythical = pokemonA1[0].Mythical;
            p.mega = pokemonA1[0].Mega;
            p.ultraBeast = pokemonA1[0].UltraBeast;
            p.generation = Convert.ToInt32(pokemonA1[0].Gen);
            p.image = pokemonA1[0].Sprite.ToString();
            p.description = pokemonA1[0].Description;
            p.captured = false;
            p.level = 20;
            p.experience = 0;

            p.types = new List<string>();
            p.evolutionLine = new List<string>();

            foreach (string t in pokemonA1[0].Types)
            {
                p.types.Add(t);
            }

       

            foreach (string evo in pokemonA1[0].Family.EvolutionLine)
            {
                p.evolutionLine.Add(evo);
            }
        }


        public static void getPokeID(int id)
        {

            var client = new RestClient("https://pokeapi.glitch.me/v1/pokemon/"+id);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                newPokemon(response);
                addPokemon();
              


    
            }
        }*/

    }
}
