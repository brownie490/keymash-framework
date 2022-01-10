namespace Automation
{

    class RandomJoke
    {

        public static string GetRandomJoke()
        {

            string URL = "http://api.icndb.com";
            string Joke = API.Get(URL + "/jokes/random?firstName=KeyMash&lastName=Testing");
            

            return Joke;
        }


        public class Response
        {
            public string type { get; set; }
            public Value value { get; set; }
        }

        public class Value
        {
            public int id { get; set; }
            public string joke { get; set; }
            public object[] categories { get; set; }
        }

    }

}
