
SCRIPT_START_DIR="$PWD"

rm -rf "../lib-native/droid-aar"
mkdir -p "../lib-native/droid-aar"

cd ../lib-native/droid-aar

## https://bintray.com/cossacklabs/maven/themis/0.12.0#


curl -L --output themis-0.13.1.aar https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.aar
curl -L --output themis-0.13.1.aar.asc https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.aar.asc
curl -L --output themis-0.13.1.pom https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.pom
curl -L --output themis-0.13.1.pom.asc https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.pom.asc



# wget --output-document=themis-0.13.1.aar https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.aar
# wget --output-document=themis-0.13.1.aar.asc https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.aar.asc
# wget --output-document=themis-0.13.1.pom https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.pom
# wget --output-document=themis-0.13.1.pom.asc https://bintray.com/cossacklabs/maven/download_file?file_path=com%2Fcossacklabs%2Fcom%2Fthemis%2F0.13.1%2Fthemis-0.13.1.pom.asc

cd "$SCRIPT_START_DIR"
