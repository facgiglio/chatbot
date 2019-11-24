using System;
using Framework.Models.Attributes;

namespace Models
{
    public class Log
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Insertable, Updatable]
        public int IdUser { get; set; }
        [Insertable, Updatable]
        public DateTime Date { get; set; }
        [Insertable, Updatable]
        public string Thread { get; set; }
        [Insertable, Updatable]
        public string Level { get; set; }
        [Insertable, Updatable]
        public string Logger { get; set; }
        [Insertable, Updatable]
        public string Message { get; set; }
        [Insertable, Updatable]
        public string Exception { get; set; }
    }
}
