using System;
using System.Collections.ObjectModel;

namespace RadioArchive
{
    public class PodcastNoteListDesignModel : PodcastNoteListViewModel
    {
        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastNoteListDesignModel Instance => new PodcastNoteListDesignModel();
        #endregion
        #region Constructor

        public PodcastNoteListDesignModel()
        {
            Items = new ObservableCollection<PodcastNoteItemViewModel>()
            {
                  new PodcastNoteItemDesignModel(
                      "In accounting terms, depreciation is defined as the reduction of the " +
                                  "recorded cost of a fixed asset in a systematic manner until the value of the asset " +
                                  "becomes zero or negligible. An example of fixed assets are buildings, furniture, office equipment, machinery etc.",
                      DateTimeOffset.UtcNow,                      
                      new TimeSpan(0, 2, 13))
,

                  new PodcastNoteItemDesignModel
                  (
                      "Anxiety is a feeling of fear, dread, and uneasiness. It might cause you to sweat, feel restless and tense, and have " +
                                  "a rapid heartbeat. It can be a normal reaction to stress. For example, you might feel anxious when faced with a difficult problem " +
                                  "at work, before taking a test, or before making an important decision.",
                       DateTimeOffset.UtcNow,
                      new TimeSpan(0, 2, 13)
                  ),
                  new PodcastNoteItemDesignModel
                  (
                      "Obsessive-compulsive disorder (OCD) features a pattern of unwanted thoughts and fears (obsessions) that lead you to do repetitive behaviors " +
                                 "(compulsions). These obsessions and compulsions interfere with daily activities and cause significant distress.",
                      DateTimeOffset.UtcNow,
                      new TimeSpan(0, 2, 13)
                  ),
            };
        }
        #endregion
    }
}
