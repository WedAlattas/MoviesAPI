namespace MoviesAPI
{
    public static class ApiEndpoints
    {

        private const string ApiBase = "api";
        public static class Movies
        {
            private const string Base = $"{ApiBase}/movies";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class Identity {
            private const string Base = $"{ApiBase}/identity/";
            public const string Login = "login";
            public const string Register = "register";


        }


    }
}
