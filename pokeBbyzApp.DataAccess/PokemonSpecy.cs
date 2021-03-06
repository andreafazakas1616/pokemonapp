//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pokeBbyzApp.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PokemonSpecy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PokemonSpecy()
        {
            this.Pokemons = new HashSet<Pokemon>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type1 { get; set; }
        public Nullable<int> Type2 { get; set; }
        public string FrontImage { get; set; }
        public string BackImage { get; set; }
        public bool IsStarterPokemon { get; set; }
        public Nullable<int> Attack { get; set; }
        public Nullable<int> Defense { get; set; }
        public Nullable<int> HP { get; set; }
        public Nullable<int> Speed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pokemon> Pokemons { get; set; }
        public virtual PokemonType PokemonType { get; set; }
        public virtual PokemonType PokemonType1 { get; set; }
    }
}
