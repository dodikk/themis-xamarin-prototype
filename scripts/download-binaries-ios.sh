SCRIPT_START_DIR="$PWD"

cd "../lib-native/ios"

## TODO: maybe wrap in bash if in case of CI job failures
rm -rf Carthage
rm Cartfile.resolved

carthage update \
    --platform "ios" \
    --cache-builds


cd "$SCRIPT_START_DIR"
