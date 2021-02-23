# directory-podcast-rss
Generate a podcast rss feed for the contents of a directory.

Sample usage: dotnet run --urls "http://0.0.0.0:5000" -d "path/to/mp3-files" -i "path/to/image"

Tested with Apple Podcasts app 2.4 and AntennaPod 1.8.1.

## Help Output

directory-podcast-rss 1.0.0
Copyright (C) 2020 directory-podcast-rss

  -d, --dir        Required. Directory containing mp3 files to add to feed.

  -i, --imgpath    Required. Path to feed image.

  --help           Display this help screen.

  --version        Display version information.

