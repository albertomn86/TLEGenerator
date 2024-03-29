[![CI](https://github.com/albertomn86/TLEGenerator/actions/workflows/ci.yml/badge.svg)](https://github.com/albertomn86/TLEGenerator/actions/workflows/ci.yml)

# TLE Generator

Custom TLE file generator.

## Usage:

`tlegenerator [-i INPUT] [-o OUTPUT]`

## Optional arguments:

```
  -i INPUT      Download TLE for the specified catalog numbers.
  -o OUTPUT     Output file path.
```

## Configuration file (config.json):

| Value | Default value | Description |
|-|-|-|
| NoradUrl | https://celestrak.com/NORAD/elements/gp.php | NORAD API URL. |
| Groups | None | Groups to retrieve the TLE data. Consider add or remove groups to reduce the number of API requests and increase the performace. |
| SatellitesListPath | satellites.json | File containing the NORAD catalog numbers for the tracked satellites. Get more catalog numbers at https://www.celestrak.com/satcat/search.php |
| OutputFilePath | custom_TLE.txt | Generated file. |
| TempFolder | System user temp folder | Folder where temporary downloaded files are stored. |
| TempFilesDays | 1 | Number of days that the temporary files are valid before downloading them again. |


## Requirements:
This application needs the .NET 8.0 Runtime. You can download it here: [Download .NET 8.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime)

## Examples:

Generate a TLE file with the NORAD catalog numbers included in the file `satellites.txt`:

```
# ./tlegenerator

✓ Saved TLE for METEOR-M2 3 (57166)
✓ Saved TLE for NOAA 15 (25338)
✓ Saved TLE for NOAA 18 (28654)
✓ Saved TLE for NOAA 19 (33591)
✓ Saved TLE for ISS (ZARYA) (25544)
TLEs retrieved: 5/5
API requests: 2
Output TLE file: /Users/ea7koo/Documents/TLEGenerator/custom_TLE.txt
```

Generate the TLE file `weather.txt` with the NORAD catalog numbers `25338`, `28654`, and `33591`:

```
# ./tlegenerator -o weather.txt -i 25338,28654,33591

✓ Saved TLE for NOAA 15 (25338)
✓ Saved TLE for NOAA 18 (28654)
✓ Saved TLE for NOAA 19 (33591)
TLEs retrieved: 3/3
API requests: 1
Output TLE file: /Users/ea7koo/Documents/TLEGenerator/weather.txt
```
