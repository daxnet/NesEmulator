// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

namespace NesEmulator.Core.Mappers
{
    /// <summary>
    /// Represents that the decorated attributes are mappers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal sealed class MapperAttribute : Attribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <c>MapperAttribute</c> class.
        /// </summary>
        /// <param name="id">The id of the mapper.</param>
        /// <param name="name">The name of the mapper.</param>
        public MapperAttribute(int id, string name) => (Id, Name) = (id, name);

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the id of the mapper.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets the name of the mapper.
        /// </summary>
        public string Name { get; init; }

        #endregion Public Properties
    }
}