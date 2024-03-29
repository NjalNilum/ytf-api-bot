﻿namespace Common
{
    public static class FileHandling
    {
        /// <summary>
        /// Method finds the video id files that are not yet processed.
        /// Files with a freely definable content exist in the subfolders of 'folderToSearchIn'. The file names of these files end with
        /// "searchPattern". It is assumed that these files are somehow processed and the already processed files names are located as a
        /// string in the text file "pathToFileOfProcessedVideoIds". This method creates a list of file names that have not yet been
        /// processed and are therefore not in the file "pathToFileOfProcessedVideoIds".
        /// </summary>
        /// <param name="pathToFileOfProcessedVideoIds">This file contains the names of the processed video files.</param>
        /// <param name="folderToSearchIn">Folder contains subfolders within video files per youtube channel</param>
        /// <param name="searchPattern">Extension of the video files.</param>
        /// <returns>Returns a list within the full paths to youtube meta files (videoId files) that are not processed yet.</returns>
        public static List<string> FindNotYetProcessedVideoIdFiles(string pathToFileOfProcessedVideoIds, string folderToSearchIn, string searchPattern)
        {
            var videoIdFiles = FindVideoIdFilesInSubfolders(folderToSearchIn, searchPattern);
            var notYetProcessedFileNames = new List<string>();

            if (videoIdFiles.Count == 0)
            {
                return notYetProcessedFileNames;
            }

            if (File.Exists(pathToFileOfProcessedVideoIds))
            {
                var listOfProcessedYoutubeFiles = File.ReadAllLines(pathToFileOfProcessedVideoIds).ToList();
                videoIdFiles.ForEach(fullPathToFile =>
                {
                    if (!listOfProcessedYoutubeFiles.Contains(fullPathToFile))
                    {
                        notYetProcessedFileNames.Add(fullPathToFile);
                    }
                });
            }
            else
            {
                // If there is not yet a file that logs which youtube meta files have been processed, a file within all paths to videoId files
                // is generated. In most cases this meas that this is the first run of this method and an empty list is returned.
                File.AppendAllLines(pathToFileOfProcessedVideoIds, videoIdFiles);
                return notYetProcessedFileNames;
            }
            return notYetProcessedFileNames;
        }

        /// <summary>
        /// Returns a list of videoId files containing in the subfolders located inside folderToSearchIn.
        /// </summary>
        /// <param name="folderToSearchIn"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static List<string> FindVideoIdFilesInSubfolders(string folderToSearchIn, string searchPattern)
        {
            var subFolders = Directory.EnumerateDirectories(folderToSearchIn);
            var videoIdFiles = new List<string>();
            foreach (var subFolder in subFolders)
            {
                videoIdFiles.AddRange(Directory.EnumerateFiles(Path.Combine(folderToSearchIn, subFolder))
                                               .Where(file => file.EndsWith(searchPattern)));
            }
            return videoIdFiles;
        }

        /// <summary>
        /// This method writes the list of the names of the files that have been processed to the file within the processed files
        /// 'pathToListOfProcessedFiles".
        /// </summary>
        /// <param name="pathToListOfProcessedFiles">Full path to the list of processed file names.</param>
        /// <param name="newProcessedFiles">List of file names that have been successfully processed.</param>
        public static void AppendFilePathsToProcessedFilesList(string pathToListOfProcessedFiles, List<string> newProcessedFiles)
        {
            if (newProcessedFiles.Count > 0)
            {
                File.AppendAllLines(pathToListOfProcessedFiles, newProcessedFiles);
            }
        }

        /// <summary>
        /// Call method to trim the file within the list of file names that are processed already.
        /// Yes, this is so inefficient.
        /// </summary>
        /// <param name="pathToListOfProcessedFiles">Full path to file</param>
        /// <param name="maxEntries">Maximum file names listed in this file. Default is 500.</param>
        public static void TrimFileListOfProcessedFile(string pathToListOfProcessedFiles, int maxEntries = 500)
        {
            var lines = File.ReadAllLines(pathToListOfProcessedFiles);
            if (lines.Length > maxEntries)
            {
                var countToSkip = lines.Length - maxEntries;
                File.WriteAllLines(pathToListOfProcessedFiles, lines.Skip(countToSkip).ToArray());
            }
        }

        /// <summary>
        /// This method searches the directory "folderToSearchIn" for files that satisfy the pattern "searchPattern" and deletes found files
        /// as soon as more than "maximumFiles" exist.
        /// The files are sorted by creation date and the oldest files are deleted first. 
        /// </summary>
        /// <param name="folderToSearchIn">Folder to search in</param>
        /// <param name="searchPattern">Search pattern</param>
        /// <param name="maximumFiles">maximum files</param>
        /// <returns></returns>
        public static void RollingFileUpdater(string folderToSearchIn, string searchPattern, int maximumFiles)
        {
            var files = Directory.GetFiles(folderToSearchIn).ToList();
            var videos = VideoMetaDataFull.DeserializeFiles(files, out var notFoundFiles).OrderBy(video => video.PublishedAtRaw).ToList();

            // The list 'videos' contains all videos found and is sorted by date. Starting with the oldest video. So now we throw away old videos
            // until the number of allowed files no longer exceeds 'maximumFiles'.
            for (int i = 0; i < videos.Count - maximumFiles; i++)
            {
                var fileNameToDelete = files.First(item => item.Contains(videos[i].Id));
                File.Delete(fileNameToDelete);
            }
        }

        /// <summary>
        /// Returns a reduced string list obtained from 'listOfIds'. Mainly used by YoutubeApi.cs to check if new videos have been published.
        /// This method searches 'folderToSearchIn' for files whose names match 'searchPattern'. It then checks whether the names of the files
        /// found contain strings from the 'listOfIds' list.Finally, only strings are returned that are not yet contained in a file name that matches
        /// the search pattern.
        /// </summary>
        /// <param name="listOfIds">List to reduce. Items that are already containing as file name in folderToSearchIn are deleted.</param>
        /// <param name="folderToSearchIn">Directory to be searched.</param>
        /// <param name="searchPattern">Search pattern.</param>
        /// <returns>Reduced list of listOfIds.</returns>
        /// <exception cref="Exception">If there was found less than 0 or more than 1 element(s).</exception>
        public static List<string> ReduceListOfIds(List<string> listOfIds, string folderToSearchIn, string searchPattern)
        {
            var fileNamesMatchingSearchPattern = Directory.EnumerateFiles(folderToSearchIn)
                                                     .Where(file => file.EndsWith(searchPattern))
                                                     .ToList();
            var idsOfFilesNotYetIncludedInFolder = new List<string>();

            listOfIds.ForEach(currentId =>
                              {
                                  var elementsContainingCurrentId = fileNamesMatchingSearchPattern.FindAll(item => item.Contains(currentId));
                                  if (elementsContainingCurrentId.Count == 0)
                                  {
                                      idsOfFilesNotYetIncludedInFolder.Add(currentId);
                                  }
                                  else if (elementsContainingCurrentId.Count == 1)
                                  {
                                      // In 'folderToSearchIn' there is already a file that corresponds to the search pattern
                                      // and contains the current Id 'currentId'. So there's is nothing to do.
                                  }
                                  else
                                  {
                                      throw new Exception($"Count of elements found is {elementsContainingCurrentId.Count}. That's not possible.");
                                  }
                              });

            return idsOfFilesNotYetIncludedInFolder;
        }
    }
}
