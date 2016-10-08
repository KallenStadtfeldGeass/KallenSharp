using System;

namespace _Project_Geass.Functions
{

    internal static class AssemblyTime
    {
        #region Public Methods

        /// <summary>
        ///     Return current time
        /// </summary>
        /// <returns>
        /// </returns>
        public static float CurrentTime() => (float)DateTime.Now.Subtract(Data.Static.Objects.AssemblyLoadTime).TotalMilliseconds;

        #endregion Public Methods
    }

}