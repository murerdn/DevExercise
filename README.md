# DevExercise
This program reads a json file and updates the version section depending on the requested release type.

## Usage
```bash
$ DevExercise.exe [options]  
```

```bash
Options:
  -r, --ReleaseType          Release type. Minor or Patch.  
  -f, --FilePath <path>      Path of input file.  
  ---help                    Display help screen.
  --version                  Display version information of application.  
```

## Example
```bash
$ DevExercise.exe -r Minor -f "D:\ProjectDetails.json"
```
