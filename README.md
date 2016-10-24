# Transcoder [![GitHub license](https://img.shields.io/github/license/nekno/transcoder.svg)](https://raw.githubusercontent.com/nekno/Transcoder/master/LICENSE) [![GitHub release](https://img.shields.io/github/release/nekno/transcoder.svg)](https://github.com/nekno/Transcoder/releases)

A simple media transcoder for converting audio/video files. For now, it is an AAC audio transcoder that converts various formats to AAC/M4A using the Apple QuickTime encoder.

It works on Windows 64-bit versions and is written in C# for the .Net 4.5 runtime.

Download the latest release: https://github.com/nekno/Transcoder/releases

## Why?

>With all the options for media encoders/transcoders, why would anyone embark on starting a new one?

I couldn't find one that satisified my basic requirements. If you know of one, please let me know, so I can abandon this.

This started as a simple command-line wrapper that I whipped up in about half-an-hour and has grown a bit here and there. It is a work-in-process, with my basic use cases covered and nothing else. 

Though I expect no one will read this, if you want something added or are having issues, please don't hesistate to open an issue here on GitHub.

## What are your basic requirements? 

- Takes input/output files/folders by drag-n-drop.
- When you drag in a folder, it remebers the folder name and automatically puts the output files in a subfolder of the same name.
- It uses the QuickTime AAC encoder, because it is essentially the best quality available.
- It avoids writing files to disk whenever possible, and does as much as possible of the decoding/encoding in memory.
- It supports decoding RAW PCM, WAV, ALAC, FLAC, MP3, AAC-LC, and other LPCM formats supported by the [qaac](https://github.com/nu774/qaac/wiki/About-input-format) project (via the Apple AudioFile service).
- It supports decoding any other audio/video formats that are supported by the [ffmpeg/libavcodec](https://www.ffmpeg.org/general.html#Audio-Codecs).

## What does it do?

### UI
- Drag-n-drop input folders anywhere on the app, and it will remember the folder you dropped in. All files in all subfolders will be added to the list. The same folder structure you dropped in will be maintained in the output. It will follow any number of levels of hierarchy.
 - So if you drop in `C:\FLAC` and it contains the following subfolders:
    - `C:\FLAC\Album1\CD1`
    - `C:\FLAC\Album1\CD2`
    - `C:\FLAC\Album2`
 - And then set the `Output` folder to `C:\MP4`, the app will create the following output folders:
    - `C:\MP4\Album1\CD1`
    - `C:\MP4\Album2\CD2`
    - `C:\MP4\Album2`
- Drag-n-drop individual
- Drag-n-drop the output folder on the `Output` textbox, or use the `Browse...` button to select the output.
- It defaults to a bitrate of 192kbps CVBR (Constrained Variable Bit Rate). You can change the bitrate to any value from 64-320kbps. The QuickTime CVBR mode is the same used by Apple for iTunes Plus on the iTunes Music Store, except Apple uses 256kbps. Higher bitrates are usually better, but if you're archiving an original copy of an audio file, and just transcoding this copy of the file for everyday listening purposes, then you should achieve a transparent encoding indistinguishable from the original by human ears at ~192kbps. 
- You can select one or more rows to delete the files from the list by clicking and dragging (or Ctrl+Clicking, Shift+Clicking) and hitting the Delete key.
- Click the `Go` button to begin transcoding the entire list of files from the top. Existing files of the same name will be overwritten.
- During transcoding, the row of the file being processed is highlighted. The checkbox in the `Done` column will be checked when the file has finished encoding.
- Asynchronous, background processing is used for transcoding so that the UI stays responsive and processing can be interrupted at anytime by clicking the `Stop` button.
- Double-click on any row to show the output log for that file during/after transcoding.

### Decoding
- When dropping input files on the app, the MediaInfo library is used to check for audio files. If there are no audio streams detected, the file will not be added to the list. This takes some time, but proved faster than using `ffmpeg`/ffprobe` and will enable showing some basic file stats in the future.
- If you dropped on a lot of folders/files, wait a bit. There's no progress UI, but the input files are being processed asynchronously.
- The app first attempts to decode a file with qaac.
 - If decoding succeeds, the decoded audio is read in directly and encoded. A temproary output file is written to disk, then qaac rewrites the output file to optimize the layout of the MP4 container. Media tags are copied from the input file to the output file, but copying artwork is not supported.
 - If decoding fails, ffmpeg is used to decode the audio and is piped directly to qaac to encode the audio date without writing an intermediate file to disk. qaac then proceeds normally, where a temporary output file is written to disk, then the output file is rewritten to optimize the MP4 container format. Media tags are not copied from the input file.
 
### Encoding
- Multi-threaded encoding is enabled with qaac.
- Gapless playback hints are written to the file in both iTunes and ISO formats.

## System requirements

The app works on 64-bit versions of Windows because it uses 64-bit executables/libraries from 3rd parties. I haven't taken the time to detect 32-bit platforms and swap in the appropriate executables/libraries, but if you know what you're doing, there's nothing special going on that would prevent you from copying in 32-bit versions of the depedencies.

- [.Net 4.5 or greater](http://go.microsoft.com/fwlink/?LinkId=780597) (currently .Net 4.6.2 is the latest)
- Apple Application Support / [iTunes](https://www.apple.com/itunes/download/)

## Dependencies

All dependencies are included with the app, except for the Apple Application Support module, which is available as a separate installer that can be extracted form the iTunes installer, or is installed automatically with iTunes. 

The easiest way to get going is to just install the latest 64-bit version of [iTunes](https://www.apple.com/itunes/download/).

- The [`qaac`](https://sites.google.com/site/qaacpage/) command-line wrapper is used for decoding and encoding with the Apple QuickTime AAC encoder. 
 - qaac [requires](https://github.com/nu774/qaac/wiki/Installation) that you have installed Apple Application Support, which is included in iTunes or QuickTime.
 - [libFLAC](http://www.rarewares.org/lossless.php) has been included for use with qaac in decoding FLAC files.
 - [libsndfile](http://www.mega-nerd.com/libsndfile) has been included for use with qaac in decoding other formats.
- [FFmpeg](http://ffmpeg.zeranoe.com/builds/) is used for fallback, decoding formats not supported by qaac.
- [MediaInfo](https://mediaarea.net/en/MediaInfo) is used to speed up determining whether files dropped on the app are audio files. 
