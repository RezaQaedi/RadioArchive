﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RadioArchive
{
    public class LastShowsViewModel : BaseViewModel
    {
        public ObservableCollection<PodcastListViewModel> Items { get; set; }

        public LastShowsViewModel()
        {
        }
    }
}
