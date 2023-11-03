namespace BlazorCLIMB.UI.Services
{
   
        public class RouteInfo
        {
            public int Id { get; set; }                     // Identificador único de la ruta
            public string Name { get; set; }                // Nombre de la ruta
            public string Grade { get; set; }               // Grado de dificultad
            public string Location { get; set; }            // Ubicación de la ruta
            public string Type { get; set; }                // Tipo de ruta (boulder, deportiva, etc.)
            public string Description { get; set; }         // Descripción de la ruta
            public List<string> ImagesUrls { get; set; }    // Lista de URLs de imágenes (si hay)
            public DateTime DateLogged { get; set; }        // Fecha en la que se registró la ascensión
            public string Climber { get; set; }             // Nombre del escalador que logró la ascensión
                                                            // Puedes añadir más propiedades según tus necesidades o la estructura de los datos que obtienes de la API o base de datos.
        }
    

}
