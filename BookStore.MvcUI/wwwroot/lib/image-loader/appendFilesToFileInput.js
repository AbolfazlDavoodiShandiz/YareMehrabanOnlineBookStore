function AppendToFileInput(filesArray) {

    const fileInput = document.getElementById('imageLoaderFileInput');

    // Get your file ready
    //const myFileContent = ['My File Content'];
    //const myFileName = file.name;
    //const myFile = new File(myFileContent, myFileName);

    // Create a data transfer object. Similar to what you get from a `drop` event as `event.dataTransfer`
    const dataTransfer = new DataTransfer();

    filesArray.forEach(arrayItem => {

        // Add your file to the file list of the object
        dataTransfer.items.add(arrayItem.File);
    });

    // Save the file list to a new variable
    const fileList = dataTransfer.files;

    // Set your input `files` to the file list
    fileInput.files = fileList;

    console.log(fileList);
}