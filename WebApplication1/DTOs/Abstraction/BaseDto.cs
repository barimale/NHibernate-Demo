namespace Demo.API.DTOs.Abstraction
{
    /// <summary>  
    /// Represents the base data transfer object with common properties.  
    /// </summary>  
    public abstract class BaseDto
    {
        /// <summary>  
        /// Gets or sets the unique identifier for the DTO.  
        /// </summary>  
        public int Id { get; set; }

        /// <summary>  
        /// Gets or sets the version number of the DTO.  
        /// </summary>  
        public int Version { get; set; }
    }
}