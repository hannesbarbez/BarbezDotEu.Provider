// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

namespace BarbezDotEu.Provider.Interfaces
{
    /// <summary>
    /// Defines an HTTP(S) client that supports rate limiting so that a polite integration
    /// with a third-party data provider can be implemented.
    /// </summary>
    public interface IPoliteProvider
    {
        /// <summary>
        /// Returns true if querying using this <see cref="IPoliteProvider"/> will respects the limit set forth by the third-party resource.
        /// </summary>
        /// <remarks>
        /// Any other methods of this <see cref="IPoliteProvider"/> should be called only after this method was called and returned true first, ensuring this application will not be blacklisted or will start receiving errors.
        /// </remarks>
        public bool IsPolite();

        /// <summary>
        /// Sets a multiplier intended for cases where multiple calls to the third-party resource have to be performed in rapid succession (a batch).
        /// In such cases, the multiplier should be set to the number of calls that were performed in batch.
        /// The multiplier is reset to 1 after the next call to <see cref="IsPolite"/> returns true.
        /// </summary>
        /// <remarks>
        /// E.g. if ordinary 1 call per minute can be made, but 2 are made in batch, set the multiplier to 2.
        /// This way, the next batch will only be allowed to run in 2 minutes' time, thus still respecting the average rate limit of 1 call per minute.
        /// </remarks>
        public void SetMultiplier(long multiplier);
    }
}
