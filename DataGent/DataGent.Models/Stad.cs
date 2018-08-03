using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataGent.Models
{
    [NotMapped]
    public class Stad
    {
        public int Id { get; set; }

        [JsonProperty(propertyName: "NAAM")]
        [Required]
        public string Naam { get; set; } //"NAAM"
        [JsonProperty(propertyName: "STRAAT")]
        public string Straat { get; set; }
        [JsonProperty(propertyName: "NUMMER")]
        public string Nummer { get; set; }
        [JsonProperty(propertyName: "POSTCODE")]
        public string Postcode { get; set; }
        [JsonProperty(propertyName: "GEMEENTE")]
        public string Gemeente { get; set; }
        [JsonProperty(propertyName: "WEBADRES")]
        [Url]
        public string Webadres { get; set; } //"WEBADRES"
        [JsonProperty(propertyName: "telefoon")]
        public string Telefoon { get; set; } //"telefoon
        [JsonProperty(propertyName: "Categorie")]
        public string Categorie { get; set; }
        [JsonProperty(propertyName: "LABEL")]
        public string Label { get; set; }
        [JsonProperty(propertyName: "opmerkingen")]
        public string Opmerkingen { get; set; }
        [JsonProperty(propertyName: "lat")]
        public string Lat { get; set; } //"lat"
        [JsonProperty(propertyName: "lng")]
        public string Long { get; set; }//"lng"

        //Navigatie properties
    }
}
