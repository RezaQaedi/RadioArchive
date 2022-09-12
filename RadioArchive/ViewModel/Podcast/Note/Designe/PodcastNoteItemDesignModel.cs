using System;

namespace RadioArchive
{
    public class PodcastNoteItemDesignModel : PodcastNoteItemViewModel
    {
        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastNoteItemDesignModel Instance =>
            new(
                text: "Anxiety is a feeling of fear, dread, and uneasiness. It might cause you to sweat, feel restless and tense, and have " +
                                  "a rapid heartbeat. It can be a normal reaction to stress. For example, you might feel anxious when faced with a difficult problem " +
                                  "at work, before taking a test, or before making an important decision.",

                date: DateTimeOffset.UtcNow,
                 new TimeSpan(0, 2, 13)
              );
        #endregion

        #region Constructor

        public PodcastNoteItemDesignModel(string text, DateTimeOffset date, TimeSpan showTime = default) : base(text, date, showTime)
        {

        }

        #endregion
    }
}
