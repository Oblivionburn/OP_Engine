﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Characters
{
    public class CharacterManager : GameComponent
    {
        #region Variables

        public static List<string> FirstNames_Male = new List<string>();
        public static List<string> FirstNames_Female = new List<string>();
        public static List<string> LastNames = new List<string>();

        public static List<Army> Armies = new List<Army>();

        #endregion

        #region Constructor

        public CharacterManager(Game game) : base(game)
        {
            LoadNames();
        }

        #endregion

        #region Methods

        public static Army GetArmy(long id)
        {
            foreach (Army army in Armies)
            {
                if (army.ID == id)
                {
                    return army;
                }
            }

            return null;
        }

        public static Army GetArmy(string name)
        {
            foreach (Army army in Armies)
            {
                if (army.Name == name)
                {
                    return army;
                }
            }

            return null;
        }

        public static Army GetArmy_ByType(string type)
        {
            foreach (Army army in Armies)
            {
                if (army.Type == type)
                {
                    return army;
                }
            }

            return null;
        }

        public static void LoadNames()
        {
            LoadFirstNames_Female();
            LoadFirstNames_Male();
            LoadLastNames();
        }

        private static void LoadFirstNames_Male()
        {
            FirstNames_Male.Add("Aaron");
            FirstNames_Male.Add("Abel");
            FirstNames_Male.Add("Adam");
            FirstNames_Male.Add("Adrian");
            FirstNames_Male.Add("Al");
            FirstNames_Male.Add("Albert");
            FirstNames_Male.Add("Aldo");
            FirstNames_Male.Add("Alex");
            FirstNames_Male.Add("Alfred");
            FirstNames_Male.Add("Allen");
            FirstNames_Male.Add("Andrew");
            FirstNames_Male.Add("Andy");
            FirstNames_Male.Add("Anthony");
            FirstNames_Male.Add("Arden");
            FirstNames_Male.Add("Arnold");
            FirstNames_Male.Add("Arthur");
            FirstNames_Male.Add("Barry");
            FirstNames_Male.Add("Bart");
            FirstNames_Male.Add("Ben");
            FirstNames_Male.Add("Benjamin");
            FirstNames_Male.Add("Bernard");
            FirstNames_Male.Add("Bert");
            FirstNames_Male.Add("Bill");
            FirstNames_Male.Add("Billy");
            FirstNames_Male.Add("Blaine");
            FirstNames_Male.Add("Blake");
            FirstNames_Male.Add("Bob");
            FirstNames_Male.Add("Brad");
            FirstNames_Male.Add("Bradley");
            FirstNames_Male.Add("Brandon");
            FirstNames_Male.Add("Brent");
            FirstNames_Male.Add("Brett");
            FirstNames_Male.Add("Brian");
            FirstNames_Male.Add("Brock");
            FirstNames_Male.Add("Bruce");
            FirstNames_Male.Add("Bryan");
            FirstNames_Male.Add("Buck");
            FirstNames_Male.Add("Bud");
            FirstNames_Male.Add("Burt");
            FirstNames_Male.Add("Buster");
            FirstNames_Male.Add("Byron");
            FirstNames_Male.Add("Caleb");
            FirstNames_Male.Add("Calvin");
            FirstNames_Male.Add("Carl");
            FirstNames_Male.Add("Carter");
            FirstNames_Male.Add("Cedric");
            FirstNames_Male.Add("Chad");
            FirstNames_Male.Add("Charles");
            FirstNames_Male.Add("Charlie");
            FirstNames_Male.Add("Chase");
            FirstNames_Male.Add("Chester");
            FirstNames_Male.Add("Chet");
            FirstNames_Male.Add("Chris");
            FirstNames_Male.Add("Christian");
            FirstNames_Male.Add("Christopher");
            FirstNames_Male.Add("Chuck");
            FirstNames_Male.Add("Clarence");
            FirstNames_Male.Add("Clark");
            FirstNames_Male.Add("Claude");
            FirstNames_Male.Add("Clay");
            FirstNames_Male.Add("Clayton");
            FirstNames_Male.Add("Cletus");
            FirstNames_Male.Add("Cleveland");
            FirstNames_Male.Add("Cliff");
            FirstNames_Male.Add("Clifford");
            FirstNames_Male.Add("Clifton");
            FirstNames_Male.Add("Clint");
            FirstNames_Male.Add("Clyde");
            FirstNames_Male.Add("Cody");
            FirstNames_Male.Add("Colby");
            FirstNames_Male.Add("Cole");
            FirstNames_Male.Add("Collin");
            FirstNames_Male.Add("Colton");
            FirstNames_Male.Add("Conrad");
            FirstNames_Male.Add("Craig");
            FirstNames_Male.Add("Curt");
            FirstNames_Male.Add("Curtis");
            FirstNames_Male.Add("Cyrus");
            FirstNames_Male.Add("Dale");
            FirstNames_Male.Add("Dallas");
            FirstNames_Male.Add("Dalton");
            FirstNames_Male.Add("Damien");
            FirstNames_Male.Add("Damon");
            FirstNames_Male.Add("Dan");
            FirstNames_Male.Add("Dane");
            FirstNames_Male.Add("Daniel");
            FirstNames_Male.Add("Dante");
            FirstNames_Male.Add("Darren");
            FirstNames_Male.Add("Daryl");
            FirstNames_Male.Add("Dave");
            FirstNames_Male.Add("David");
            FirstNames_Male.Add("Davis");
            FirstNames_Male.Add("Dean");
            FirstNames_Male.Add("Del");
            FirstNames_Male.Add("Delbert");
            FirstNames_Male.Add("Dennis");
            FirstNames_Male.Add("Derek");
            FirstNames_Male.Add("Devin");
            FirstNames_Male.Add("Dewey");
            FirstNames_Male.Add("Dexter");
            FirstNames_Male.Add("Dick");
            FirstNames_Male.Add("Dillon");
            FirstNames_Male.Add("Dion");
            FirstNames_Male.Add("Dominic");
            FirstNames_Male.Add("Don");
            FirstNames_Male.Add("Donald");
            FirstNames_Male.Add("Doug");
            FirstNames_Male.Add("Douglas");
            FirstNames_Male.Add("Doyle");
            FirstNames_Male.Add("Drew");
            FirstNames_Male.Add("Dudley");
            FirstNames_Male.Add("Duncan");
            FirstNames_Male.Add("Dustin");
            FirstNames_Male.Add("Dusty");
            FirstNames_Male.Add("Dwayne");
            FirstNames_Male.Add("Dwight");
            FirstNames_Male.Add("Dylan");
            FirstNames_Male.Add("Earl");
            FirstNames_Male.Add("Ed");
            FirstNames_Male.Add("Eddie");
            FirstNames_Male.Add("Edgar");
            FirstNames_Male.Add("Edmond");
            FirstNames_Male.Add("Edward");
            FirstNames_Male.Add("Edwin");
            FirstNames_Male.Add("Eli");
            FirstNames_Male.Add("Elias");
            FirstNames_Male.Add("Elijah");
            FirstNames_Male.Add("Elliot");
            FirstNames_Male.Add("Ellis");
            FirstNames_Male.Add("Elmer");
            FirstNames_Male.Add("Elroy");
            FirstNames_Male.Add("Elton");
            FirstNames_Male.Add("Elvis");
            FirstNames_Male.Add("Emanuel");
            FirstNames_Male.Add("Emery");
            FirstNames_Male.Add("Eric");
            FirstNames_Male.Add("Ernest");
            FirstNames_Male.Add("Ernie");
            FirstNames_Male.Add("Erwin");
            FirstNames_Male.Add("Ethan");
            FirstNames_Male.Add("Eugene");
            FirstNames_Male.Add("Evan");
            FirstNames_Male.Add("Fletcher");
            FirstNames_Male.Add("Floyd");
            FirstNames_Male.Add("Forrest");
            FirstNames_Male.Add("Foster");
            FirstNames_Male.Add("Frances");
            FirstNames_Male.Add("Frank");
            FirstNames_Male.Add("Franklin");
            FirstNames_Male.Add("Fred");
            FirstNames_Male.Add("Freddy");
            FirstNames_Male.Add("Fredrick");
            FirstNames_Male.Add("Gabriel");
            FirstNames_Male.Add("Garret");
            FirstNames_Male.Add("Gary");
            FirstNames_Male.Add("Gavin");
            FirstNames_Male.Add("George");
            FirstNames_Male.Add("Gerald");
            FirstNames_Male.Add("Gilbert");
            FirstNames_Male.Add("Glen");
            FirstNames_Male.Add("Gordon");
            FirstNames_Male.Add("Grady");
            FirstNames_Male.Add("Graham");
            FirstNames_Male.Add("Grant");
            FirstNames_Male.Add("Greg");
            FirstNames_Male.Add("Gregory");
            FirstNames_Male.Add("Guy");
            FirstNames_Male.Add("Hank");
            FirstNames_Male.Add("Harland");
            FirstNames_Male.Add("Harley");
            FirstNames_Male.Add("Harold");
            FirstNames_Male.Add("Harris");
            FirstNames_Male.Add("Harrison");
            FirstNames_Male.Add("Harry");
            FirstNames_Male.Add("Harvey");
            FirstNames_Male.Add("Hayden");
            FirstNames_Male.Add("Heath");
            FirstNames_Male.Add("Hector");
            FirstNames_Male.Add("Henry");
            FirstNames_Male.Add("Herman");
            FirstNames_Male.Add("Homer");
            FirstNames_Male.Add("Howard");
            FirstNames_Male.Add("Hubert");
            FirstNames_Male.Add("Huey");
            FirstNames_Male.Add("Hugh");
            FirstNames_Male.Add("Hunter");
            FirstNames_Male.Add("Ian");
            FirstNames_Male.Add("Irving");
            FirstNames_Male.Add("Irwin");
            FirstNames_Male.Add("Ivan");
            FirstNames_Male.Add("Jack");
            FirstNames_Male.Add("Jacob");
            FirstNames_Male.Add("Jake");
            FirstNames_Male.Add("James");
            FirstNames_Male.Add("Jared");
            FirstNames_Male.Add("Jason");
            FirstNames_Male.Add("Jeff");
            FirstNames_Male.Add("Jeffery");
            FirstNames_Male.Add("Jeremy");
            FirstNames_Male.Add("Jerry");
            FirstNames_Male.Add("Jim");
            FirstNames_Male.Add("Jimmy");
            FirstNames_Male.Add("Joe");
            FirstNames_Male.Add("Joel");
            FirstNames_Male.Add("Joseph");
            FirstNames_Male.Add("Joey");
            FirstNames_Male.Add("John");
            FirstNames_Male.Add("Jordan");
            FirstNames_Male.Add("Josh");
            FirstNames_Male.Add("Jude");
            FirstNames_Male.Add("Julian");
            FirstNames_Male.Add("Justin");
            FirstNames_Male.Add("Karl");
            FirstNames_Male.Add("Keith");
            FirstNames_Male.Add("Kelvin");
            FirstNames_Male.Add("Kenneth");
            FirstNames_Male.Add("Kenny");
            FirstNames_Male.Add("Kent");
            FirstNames_Male.Add("Kevin");
            FirstNames_Male.Add("Kurt");
            FirstNames_Male.Add("Kurtis");
            FirstNames_Male.Add("Kyle");
            FirstNames_Male.Add("Lance");
            FirstNames_Male.Add("Landon");
            FirstNames_Male.Add("Larry");
            FirstNames_Male.Add("Laurence");
            FirstNames_Male.Add("Lee");
            FirstNames_Male.Add("Leland");
            FirstNames_Male.Add("Lenard");
            FirstNames_Male.Add("Leo");
            FirstNames_Male.Add("Lester");
            FirstNames_Male.Add("Lewis");
            FirstNames_Male.Add("Lincoln");
            FirstNames_Male.Add("Lionel");
            FirstNames_Male.Add("Lloyd");
            FirstNames_Male.Add("Logan");
            FirstNames_Male.Add("Louis");
            FirstNames_Male.Add("Lucas");
            FirstNames_Male.Add("Luke");
            FirstNames_Male.Add("Mack");
            FirstNames_Male.Add("Malcolm");
            FirstNames_Male.Add("Mark");
            FirstNames_Male.Add("Martin");
            FirstNames_Male.Add("Marvin");
            FirstNames_Male.Add("Mason");
            FirstNames_Male.Add("Mathew");
            FirstNames_Male.Add("Matt");
            FirstNames_Male.Add("Maurice");
            FirstNames_Male.Add("Max");
            FirstNames_Male.Add("Maxwell");
            FirstNames_Male.Add("Maynard");
            FirstNames_Male.Add("Melvin");
            FirstNames_Male.Add("Merrill");
            FirstNames_Male.Add("Michael");
            FirstNames_Male.Add("Mike");
            FirstNames_Male.Add("Miles");
            FirstNames_Male.Add("Milford");
            FirstNames_Male.Add("Milo");
            FirstNames_Male.Add("Milton");
            FirstNames_Male.Add("Mitch");
            FirstNames_Male.Add("Mitchell");
            FirstNames_Male.Add("Monty");
            FirstNames_Male.Add("Murray");
            FirstNames_Male.Add("Myles");
            FirstNames_Male.Add("Nathan");
            FirstNames_Male.Add("Neil");
            FirstNames_Male.Add("Ned");
            FirstNames_Male.Add("Nelson");
            FirstNames_Male.Add("Nick");
            FirstNames_Male.Add("Norbert");
            FirstNames_Male.Add("Norman");
            FirstNames_Male.Add("Oliver");
            FirstNames_Male.Add("Oscar");
            FirstNames_Male.Add("Otis");
            FirstNames_Male.Add("Otto");
            FirstNames_Male.Add("Owen");
            FirstNames_Male.Add("Palmer");
            FirstNames_Male.Add("Parker");
            FirstNames_Male.Add("Paul");
            FirstNames_Male.Add("Pete");
            FirstNames_Male.Add("Peter");
            FirstNames_Male.Add("Phil");
            FirstNames_Male.Add("Phillip");
            FirstNames_Male.Add("Porter");
            FirstNames_Male.Add("Preston");
            FirstNames_Male.Add("Quincy");
            FirstNames_Male.Add("Quinton");
            FirstNames_Male.Add("Ralph");
            FirstNames_Male.Add("Randall");
            FirstNames_Male.Add("Randolph");
            FirstNames_Male.Add("Randy");
            FirstNames_Male.Add("Ray");
            FirstNames_Male.Add("Raymond");
            FirstNames_Male.Add("Rex");
            FirstNames_Male.Add("Rich");
            FirstNames_Male.Add("Richard");
            FirstNames_Male.Add("Rick");
            FirstNames_Male.Add("Rob");
            FirstNames_Male.Add("Robert");
            FirstNames_Male.Add("Rocky");
            FirstNames_Male.Add("Rodney");
            FirstNames_Male.Add("Rodrick");
            FirstNames_Male.Add("Roger");
            FirstNames_Male.Add("Rolland");
            FirstNames_Male.Add("Romeo");
            FirstNames_Male.Add("Ron");
            FirstNames_Male.Add("Ronald");
            FirstNames_Male.Add("Ross");
            FirstNames_Male.Add("Roy");
            FirstNames_Male.Add("Rudy");
            FirstNames_Male.Add("Rupert");
            FirstNames_Male.Add("Russel");
            FirstNames_Male.Add("Rusty");
            FirstNames_Male.Add("Ryan");
            FirstNames_Male.Add("Sam");
            FirstNames_Male.Add("Samuel");
            FirstNames_Male.Add("Scott");
            FirstNames_Male.Add("Sean");
            FirstNames_Male.Add("Sebastian");
            FirstNames_Male.Add("Seth");
            FirstNames_Male.Add("Seymour");
            FirstNames_Male.Add("Shane");
            FirstNames_Male.Add("Sheldon");
            FirstNames_Male.Add("Shelton");
            FirstNames_Male.Add("Sherman");
            FirstNames_Male.Add("Sid");
            FirstNames_Male.Add("Sidney");
            FirstNames_Male.Add("Simon");
            FirstNames_Male.Add("Spencer");
            FirstNames_Male.Add("Stan");
            FirstNames_Male.Add("Stanford");
            FirstNames_Male.Add("Stanley");
            FirstNames_Male.Add("Stanton");
            FirstNames_Male.Add("Steve");
            FirstNames_Male.Add("Steven");
            FirstNames_Male.Add("Stewart");
            FirstNames_Male.Add("Taylor");
            FirstNames_Male.Add("Ted");
            FirstNames_Male.Add("Teddy");
            FirstNames_Male.Add("Terrance");
            FirstNames_Male.Add("Thad");
            FirstNames_Male.Add("Theo");
            FirstNames_Male.Add("Theodore");
            FirstNames_Male.Add("Thomas");
            FirstNames_Male.Add("Tim");
            FirstNames_Male.Add("Timmy");
            FirstNames_Male.Add("Timothy");
            FirstNames_Male.Add("Toby");
            FirstNames_Male.Add("Todd");
            FirstNames_Male.Add("Tom");
            FirstNames_Male.Add("Tommy");
            FirstNames_Male.Add("Tony");
            FirstNames_Male.Add("Travis");
            FirstNames_Male.Add("Trent");
            FirstNames_Male.Add("Trenton");
            FirstNames_Male.Add("Trevor");
            FirstNames_Male.Add("Tristan");
            FirstNames_Male.Add("Troy");
            FirstNames_Male.Add("Truman");
            FirstNames_Male.Add("Tyler");
            FirstNames_Male.Add("Tyson");
            FirstNames_Male.Add("Vance");
            FirstNames_Male.Add("Vaughn");
            FirstNames_Male.Add("Vernon");
            FirstNames_Male.Add("Victor");
            FirstNames_Male.Add("Vince");
            FirstNames_Male.Add("Vincent");
            FirstNames_Male.Add("Virgil");
            FirstNames_Male.Add("Wade");
            FirstNames_Male.Add("Waldo");
            FirstNames_Male.Add("Walker");
            FirstNames_Male.Add("Wallace");
            FirstNames_Male.Add("Wally");
            FirstNames_Male.Add("Walter");
            FirstNames_Male.Add("Walton");
            FirstNames_Male.Add("Ward");
            FirstNames_Male.Add("Warren");
            FirstNames_Male.Add("Wayne");
            FirstNames_Male.Add("Wesley");
            FirstNames_Male.Add("Wilber");
            FirstNames_Male.Add("Wilbert");
            FirstNames_Male.Add("Wilford");
            FirstNames_Male.Add("Will");
            FirstNames_Male.Add("Willard");
            FirstNames_Male.Add("William");
            FirstNames_Male.Add("Willy");
            FirstNames_Male.Add("Wilmer");
            FirstNames_Male.Add("Wilson");
            FirstNames_Male.Add("Winston");
            FirstNames_Male.Add("Wyatt");
            FirstNames_Male.Add("Zack");
        }

        private static void LoadFirstNames_Female()
        {
            FirstNames_Female.Add("Mary");
            FirstNames_Female.Add("Patricia");
            FirstNames_Female.Add("Linda");
            FirstNames_Female.Add("Barbara");
            FirstNames_Female.Add("Elizabeth");
            FirstNames_Female.Add("Jennifer");
            FirstNames_Female.Add("Maria");
            FirstNames_Female.Add("Susan");
            FirstNames_Female.Add("Margaret");
            FirstNames_Female.Add("Dorothy");
            FirstNames_Female.Add("Lisa");
            FirstNames_Female.Add("Nancy");
            FirstNames_Female.Add("Karen");
            FirstNames_Female.Add("Betty");
            FirstNames_Female.Add("Helen");
            FirstNames_Female.Add("Sandra");
            FirstNames_Female.Add("Donna");
            FirstNames_Female.Add("Carol");
            FirstNames_Female.Add("Ruth");
            FirstNames_Female.Add("Sharon");
            FirstNames_Female.Add("Michelle");
            FirstNames_Female.Add("Laura");
            FirstNames_Female.Add("Sarah");
            FirstNames_Female.Add("Kimberly");
            FirstNames_Female.Add("Deborah");
            FirstNames_Female.Add("Jessica");
            FirstNames_Female.Add("Shirley");
            FirstNames_Female.Add("Cynthia");
            FirstNames_Female.Add("Angela");
            FirstNames_Female.Add("Melissa");
            FirstNames_Female.Add("Brenda");
            FirstNames_Female.Add("Amy");
            FirstNames_Female.Add("Anna");
            FirstNames_Female.Add("Rebecca");
            FirstNames_Female.Add("Virginia");
            FirstNames_Female.Add("Kathleen");
            FirstNames_Female.Add("Pamela");
            FirstNames_Female.Add("Martha");
            FirstNames_Female.Add("Debra");
            FirstNames_Female.Add("Amanda");
            FirstNames_Female.Add("Stephanie");
            FirstNames_Female.Add("Carolyn");
            FirstNames_Female.Add("Christine");
            FirstNames_Female.Add("Janet");
            FirstNames_Female.Add("Catherine");
            FirstNames_Female.Add("Frances");
            FirstNames_Female.Add("Ann");
            FirstNames_Female.Add("Joyce");
            FirstNames_Female.Add("Diane");
            FirstNames_Female.Add("Alice");
            FirstNames_Female.Add("Julie");
            FirstNames_Female.Add("Heather");
            FirstNames_Female.Add("Doris");
            FirstNames_Female.Add("Gloria");
            FirstNames_Female.Add("Evelyn");
            FirstNames_Female.Add("Jean");
            FirstNames_Female.Add("Cheryl");
            FirstNames_Female.Add("Mildred");
            FirstNames_Female.Add("Katherine");
            FirstNames_Female.Add("Joan");
            FirstNames_Female.Add("Ashley");
            FirstNames_Female.Add("Judith");
            FirstNames_Female.Add("Rose");
            FirstNames_Female.Add("Janice");
            FirstNames_Female.Add("Kelly");
            FirstNames_Female.Add("Nicole");
            FirstNames_Female.Add("Judy");
            FirstNames_Female.Add("Christina");
            FirstNames_Female.Add("Kathy");
            FirstNames_Female.Add("Theresa");
            FirstNames_Female.Add("Beverly");
            FirstNames_Female.Add("Denise");
            FirstNames_Female.Add("Tammy");
            FirstNames_Female.Add("Irene");
            FirstNames_Female.Add("Jane");
            FirstNames_Female.Add("Lori");
            FirstNames_Female.Add("Rachel");
            FirstNames_Female.Add("Marilyn");
            FirstNames_Female.Add("Andrea");
            FirstNames_Female.Add("Kathryn");
            FirstNames_Female.Add("Louise");
            FirstNames_Female.Add("Sara");
            FirstNames_Female.Add("Anne");
            FirstNames_Female.Add("Jacqueline");
            FirstNames_Female.Add("Wanda");
            FirstNames_Female.Add("Bonnie");
            FirstNames_Female.Add("Julia");
            FirstNames_Female.Add("Ruby");
            FirstNames_Female.Add("Tina");
            FirstNames_Female.Add("Norma");
            FirstNames_Female.Add("Paula");
            FirstNames_Female.Add("Diana");
            FirstNames_Female.Add("Annie");
            FirstNames_Female.Add("Lillian");
            FirstNames_Female.Add("Emily");
            FirstNames_Female.Add("Robin");
            FirstNames_Female.Add("Peggy");
            FirstNames_Female.Add("Crystal");
            FirstNames_Female.Add("Gladys");
            FirstNames_Female.Add("Rita");
            FirstNames_Female.Add("Dawn");
            FirstNames_Female.Add("Connie");
            FirstNames_Female.Add("Tracy");
            FirstNames_Female.Add("Edna");
            FirstNames_Female.Add("Tiffany");
            FirstNames_Female.Add("Carmen");
            FirstNames_Female.Add("Rosa");
            FirstNames_Female.Add("Cindy");
            FirstNames_Female.Add("Grace");
            FirstNames_Female.Add("Wendy");
            FirstNames_Female.Add("Victoria");
            FirstNames_Female.Add("Edith");
            FirstNames_Female.Add("Kim");
            FirstNames_Female.Add("Sherry");
            FirstNames_Female.Add("Sylvia");
            FirstNames_Female.Add("Josephine");
            FirstNames_Female.Add("Thelma");
            FirstNames_Female.Add("Shannon");
            FirstNames_Female.Add("Sheila");
            FirstNames_Female.Add("Ethel");
            FirstNames_Female.Add("Ellen");
            FirstNames_Female.Add("Elaine");
            FirstNames_Female.Add("Marjorie");
            FirstNames_Female.Add("Carrie");
            FirstNames_Female.Add("Charlotte");
            FirstNames_Female.Add("Monica");
            FirstNames_Female.Add("Esther");
            FirstNames_Female.Add("Pauline");
            FirstNames_Female.Add("Emma");
            FirstNames_Female.Add("Anita");
            FirstNames_Female.Add("Rhonda");
            FirstNames_Female.Add("Hazel");
            FirstNames_Female.Add("Amber");
            FirstNames_Female.Add("Eva");
            FirstNames_Female.Add("Debbie");
            FirstNames_Female.Add("April");
            FirstNames_Female.Add("Leslie");
            FirstNames_Female.Add("Clara");
            FirstNames_Female.Add("Lucille");
            FirstNames_Female.Add("Jamie");
            FirstNames_Female.Add("Joanne");
            FirstNames_Female.Add("Eleanor");
            FirstNames_Female.Add("Valerie");
            FirstNames_Female.Add("Danielle");
            FirstNames_Female.Add("Megan");
            FirstNames_Female.Add("Alicia");
            FirstNames_Female.Add("Suzanne");
            FirstNames_Female.Add("Gail");
            FirstNames_Female.Add("Bertha");
            FirstNames_Female.Add("Darlene");
            FirstNames_Female.Add("Veronica");
            FirstNames_Female.Add("Jill");
            FirstNames_Female.Add("Erin");
            FirstNames_Female.Add("Geraldine");
            FirstNames_Female.Add("Lauren");
            FirstNames_Female.Add("Cathy");
            FirstNames_Female.Add("Lorraine");
            FirstNames_Female.Add("Lynn");
            FirstNames_Female.Add("Sally");
            FirstNames_Female.Add("Regina");
            FirstNames_Female.Add("Erica");
            FirstNames_Female.Add("Beatrice");
            FirstNames_Female.Add("Dolores");
            FirstNames_Female.Add("Bernice");
            FirstNames_Female.Add("Audrey");
            FirstNames_Female.Add("Annette");
            FirstNames_Female.Add("June");
            FirstNames_Female.Add("Samantha");
            FirstNames_Female.Add("Dana");
            FirstNames_Female.Add("Stacy");
            FirstNames_Female.Add("Renee");
            FirstNames_Female.Add("Ida");
            FirstNames_Female.Add("Vivian");
            FirstNames_Female.Add("Roberta");
            FirstNames_Female.Add("Holly");
            FirstNames_Female.Add("Brittany");
            FirstNames_Female.Add("Melanie");
            FirstNames_Female.Add("Loretta");
            FirstNames_Female.Add("Jeanette");
            FirstNames_Female.Add("Laurie");
            FirstNames_Female.Add("Katie");
            FirstNames_Female.Add("Kristen");
            FirstNames_Female.Add("Vanessa");
            FirstNames_Female.Add("Alma");
            FirstNames_Female.Add("Sue");
            FirstNames_Female.Add("Elsie");
            FirstNames_Female.Add("Beth");
            FirstNames_Female.Add("Vicki");
            FirstNames_Female.Add("Carla");
            FirstNames_Female.Add("Tara");
            FirstNames_Female.Add("Rosemary");
            FirstNames_Female.Add("Eileen");
            FirstNames_Female.Add("Terri");
            FirstNames_Female.Add("Gertrude");
            FirstNames_Female.Add("Lucy");
            FirstNames_Female.Add("Tonya");
            FirstNames_Female.Add("Ella");
            FirstNames_Female.Add("Stacey");
            FirstNames_Female.Add("Wilma");
            FirstNames_Female.Add("Gina");
            FirstNames_Female.Add("Kristin");
            FirstNames_Female.Add("Jessie");
            FirstNames_Female.Add("Natalie");
            FirstNames_Female.Add("Agnes");
            FirstNames_Female.Add("Vera");
            FirstNames_Female.Add("Charlene");
            FirstNames_Female.Add("Bessie");
            FirstNames_Female.Add("Delores");
            FirstNames_Female.Add("Melinda");
            FirstNames_Female.Add("Pearl");
            FirstNames_Female.Add("Arlene");
            FirstNames_Female.Add("Maureen");
            FirstNames_Female.Add("Colleen");
            FirstNames_Female.Add("Allison");
            FirstNames_Female.Add("Tamara");
            FirstNames_Female.Add("Joy");
            FirstNames_Female.Add("Georgia");
            FirstNames_Female.Add("Constance");
            FirstNames_Female.Add("Lillie");
            FirstNames_Female.Add("Claudia");
            FirstNames_Female.Add("Jackie");
            FirstNames_Female.Add("Tanya");
            FirstNames_Female.Add("Nellie");
            FirstNames_Female.Add("Minnie");
            FirstNames_Female.Add("Marlene");
            FirstNames_Female.Add("Heidi");
            FirstNames_Female.Add("Glenda");
            FirstNames_Female.Add("Lydia");
            FirstNames_Female.Add("Viola");
            FirstNames_Female.Add("Courtney");
            FirstNames_Female.Add("Stella");
            FirstNames_Female.Add("Caroline");
            FirstNames_Female.Add("Dora");
            FirstNames_Female.Add("Vickie");
            FirstNames_Female.Add("Mattie");
            FirstNames_Female.Add("Terry");
            FirstNames_Female.Add("Maxine");
            FirstNames_Female.Add("Irma");
            FirstNames_Female.Add("Mabel");
            FirstNames_Female.Add("Marsha");
            FirstNames_Female.Add("Myrtle");
            FirstNames_Female.Add("Lena");
            FirstNames_Female.Add("Christy");
            FirstNames_Female.Add("Deanna");
            FirstNames_Female.Add("Patsy");
            FirstNames_Female.Add("Hilda");
            FirstNames_Female.Add("Gwendolyn");
            FirstNames_Female.Add("Nora");
            FirstNames_Female.Add("Margie");
            FirstNames_Female.Add("Nina");
            FirstNames_Female.Add("Cassandra");
            FirstNames_Female.Add("Leah");
            FirstNames_Female.Add("Penny");
            FirstNames_Female.Add("Priscilla");
            FirstNames_Female.Add("Naomi");
            FirstNames_Female.Add("Carole");
            FirstNames_Female.Add("Brandy");
            FirstNames_Female.Add("Olga");
            FirstNames_Female.Add("Dianne");
            FirstNames_Female.Add("Tracey");
            FirstNames_Female.Add("Leona");
            FirstNames_Female.Add("Jenny");
            FirstNames_Female.Add("Felicia");
            FirstNames_Female.Add("Sonia");
            FirstNames_Female.Add("Miriam");
            FirstNames_Female.Add("Velma");
            FirstNames_Female.Add("Becky");
        }

        private static void LoadLastNames()
        {
            LastNames.Add("Smith");
            LastNames.Add("Johnson");
            LastNames.Add("Williams");
            LastNames.Add("Jones");
            LastNames.Add("Brown");
            LastNames.Add("Davis");
            LastNames.Add("Miller");
            LastNames.Add("Wilson");
            LastNames.Add("Moore");
            LastNames.Add("Taylor");
            LastNames.Add("Anderson");
            LastNames.Add("Thomas");
            LastNames.Add("Jackson");
            LastNames.Add("White");
            LastNames.Add("Harris");
            LastNames.Add("Martin");
            LastNames.Add("Thompson");
            LastNames.Add("Robinson");
            LastNames.Add("Clark");
            LastNames.Add("Lewis");
            LastNames.Add("Lee");
            LastNames.Add("Walker");
            LastNames.Add("Hall");
            LastNames.Add("Allen");
            LastNames.Add("Young");
            LastNames.Add("King");
            LastNames.Add("Wright");
            LastNames.Add("Hill");
            LastNames.Add("Scott");
            LastNames.Add("Green");
            LastNames.Add("Adams");
            LastNames.Add("Baker");
            LastNames.Add("Nelson");
            LastNames.Add("Carter");
            LastNames.Add("Mitchell");
            LastNames.Add("Roberts");
            LastNames.Add("Turner");
            LastNames.Add("Phillips");
            LastNames.Add("Campbell");
            LastNames.Add("Parker");
            LastNames.Add("Evans");
            LastNames.Add("Edwards");
            LastNames.Add("Collins");
            LastNames.Add("Stewart");
            LastNames.Add("Morris");
            LastNames.Add("Rogers");
            LastNames.Add("Reed");
            LastNames.Add("Cook");
            LastNames.Add("Morgan");
            LastNames.Add("Bell");
            LastNames.Add("Murphy");
            LastNames.Add("Bailey");
            LastNames.Add("Cooper");
            LastNames.Add("Richardson");
            LastNames.Add("Cox");
            LastNames.Add("Howard");
            LastNames.Add("Ward");
            LastNames.Add("Peterson");
            LastNames.Add("Gray");
            LastNames.Add("James");
            LastNames.Add("Watson");
            LastNames.Add("Brooks");
            LastNames.Add("Kelly");
            LastNames.Add("Sanders");
            LastNames.Add("Price");
            LastNames.Add("Bennett");
            LastNames.Add("Wood");
            LastNames.Add("Barnes");
            LastNames.Add("Ross");
            LastNames.Add("Henderson");
            LastNames.Add("Coleman");
            LastNames.Add("Jenkins");
            LastNames.Add("Perry");
            LastNames.Add("Powell");
            LastNames.Add("Patterson");
            LastNames.Add("Hughes");
            LastNames.Add("Washington");
            LastNames.Add("Butler");
            LastNames.Add("Simmons");
            LastNames.Add("Foster");
            LastNames.Add("Bryant");
            LastNames.Add("Alexander");
            LastNames.Add("Russell");
            LastNames.Add("Griffin");
            LastNames.Add("Hayes");
            LastNames.Add("Myers");
            LastNames.Add("Ford");
            LastNames.Add("Hamilton");
            LastNames.Add("Graham");
            LastNames.Add("Sullivan");
            LastNames.Add("Wallace");
            LastNames.Add("Woods");
            LastNames.Add("Cole");
            LastNames.Add("West");
            LastNames.Add("Jordan");
            LastNames.Add("Owens");
            LastNames.Add("Reynolds");
            LastNames.Add("Fisher");
            LastNames.Add("Ellis");
            LastNames.Add("Harrison");
            LastNames.Add("Gibson");
            LastNames.Add("McDonald");
            LastNames.Add("Cruz");
            LastNames.Add("Marshall");
            LastNames.Add("Gomez");
            LastNames.Add("Murray");
            LastNames.Add("Freeman");
            LastNames.Add("Wells");
            LastNames.Add("Webb");
            LastNames.Add("Simpson");
            LastNames.Add("Stevens");
            LastNames.Add("Tucker");
            LastNames.Add("Porter");
            LastNames.Add("Hunter");
            LastNames.Add("Hicks");
            LastNames.Add("Crawford");
            LastNames.Add("Henry");
            LastNames.Add("Boyd");
            LastNames.Add("Mason");
            LastNames.Add("Kennedy");
            LastNames.Add("Warren");
            LastNames.Add("Dixon");
            LastNames.Add("Reyes");
            LastNames.Add("Burns");
            LastNames.Add("Gordon");
            LastNames.Add("Shaw");
            LastNames.Add("Holmes");
            LastNames.Add("Rice");
            LastNames.Add("Robertson");
            LastNames.Add("Hunt");
            LastNames.Add("Black");
            LastNames.Add("Daniels");
            LastNames.Add("Palmer");
            LastNames.Add("Mills");
            LastNames.Add("Nichols");
            LastNames.Add("Grant");
            LastNames.Add("Knight");
            LastNames.Add("Ferguson");
            LastNames.Add("Rose");
            LastNames.Add("Stone");
            LastNames.Add("Hawkins");
            LastNames.Add("Dunn");
            LastNames.Add("Perkins");
            LastNames.Add("Hudson");
            LastNames.Add("Spencer");
            LastNames.Add("Gardner");
            LastNames.Add("Payne");
            LastNames.Add("Pierce");
            LastNames.Add("Berry");
            LastNames.Add("Matthews");
            LastNames.Add("Wagner");
            LastNames.Add("Willis");
            LastNames.Add("Ray");
            LastNames.Add("Watkins");
            LastNames.Add("Olson");
            LastNames.Add("Carroll");
            LastNames.Add("Duncan");
            LastNames.Add("Snyder");
            LastNames.Add("Hart");
            LastNames.Add("Cunningham");
            LastNames.Add("Bradley");
            LastNames.Add("Lane");
            LastNames.Add("Andrews");
            LastNames.Add("Harper");
            LastNames.Add("Fox");
            LastNames.Add("Riley");
            LastNames.Add("Armstrong");
            LastNames.Add("Carpenter");
            LastNames.Add("Weaver");
            LastNames.Add("Greene");
            LastNames.Add("Lawrence");
            LastNames.Add("Elliott");
            LastNames.Add("Sims");
            LastNames.Add("Austin");
            LastNames.Add("Peters");
            LastNames.Add("Kelley");
            LastNames.Add("Franklin");
            LastNames.Add("Lawson");
            LastNames.Add("Fields");
            LastNames.Add("Ryan");
            LastNames.Add("Schmidt");
            LastNames.Add("Castillo");
            LastNames.Add("Wheeler");
            LastNames.Add("Chapman");
            LastNames.Add("Oliver");
            LastNames.Add("Montgomery");
            LastNames.Add("Richards");
            LastNames.Add("Williamson");
            LastNames.Add("Johnston");
            LastNames.Add("Banks");
            LastNames.Add("Meyer");
            LastNames.Add("Bishop");
            LastNames.Add("McCoy");
            LastNames.Add("Howell");
            LastNames.Add("Morrison");
            LastNames.Add("Hansen");
            LastNames.Add("Harvey");
            LastNames.Add("Little");
            LastNames.Add("Burton");
            LastNames.Add("Stanley");
            LastNames.Add("George");
            LastNames.Add("Jacobs");
            LastNames.Add("Reid");
            LastNames.Add("Lynch");
            LastNames.Add("Fuller");
            LastNames.Add("Dean");
            LastNames.Add("Gilbert");
            LastNames.Add("Garrett");
            LastNames.Add("Romero");
            LastNames.Add("Welch");
            LastNames.Add("Larson");
            LastNames.Add("Frazier");
            LastNames.Add("Burke");
            LastNames.Add("Hanson");
            LastNames.Add("Day");
            LastNames.Add("Moreno");
            LastNames.Add("Bowman");
            LastNames.Add("Medina");
            LastNames.Add("Fowler");
            LastNames.Add("Brewer");
            LastNames.Add("Hoffman");
            LastNames.Add("Carlson");
            LastNames.Add("Silva");
            LastNames.Add("Pearson");
            LastNames.Add("Holland");
            LastNames.Add("Douglas");
            LastNames.Add("Fleming");
            LastNames.Add("Jensen");
            LastNames.Add("Vargas");
            LastNames.Add("Byrd");
            LastNames.Add("Davidson");
            LastNames.Add("Hopkins");
        }

        #endregion
    }
}
