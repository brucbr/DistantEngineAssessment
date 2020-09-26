namespace DistantEngine.Objects.Components
{
    /// <summary>
    /// Interface for components.  It is suggested that these are implemented universally, with any ID system you desire.
    /// HOWEVER, it is recommended that you keep other publicly accessible methods to a minimum.
    /// </summary>
    public interface IGoComponent
    {
        /// <summary>
        /// Update function to be run every frame update.
        /// </summary>
        public void Update();
        
        /// <summary>
        /// Draw to renderer. Anything that needs to be drawn to the renderer will be done here.
        /// </summary>
        public void Draw();
    }
}