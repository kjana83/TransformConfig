
namespace TransformConfig
{
    /// <summary>
    /// Interface for the transform.
    /// </summary>
    interface ITransform
    {
        /// <summary>
        /// Transforms the specified source file.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="transformFile">The transform file.</param>
        string ApplyTransform(string sourceFile, string transformFile);
    }
}
