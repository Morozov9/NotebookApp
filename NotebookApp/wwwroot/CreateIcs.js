
window.downloadIcsFile = function (fileName, fileContent) {

    var blob = new Blob([fileContent], { type: 'application/octet-stream' });

    var url = URL.createObjectURL(blob);

    var anchor = document.createElement('a');

    anchor.href = url;         
    anchor.download = fileName;

    document.body.appendChild(anchor);
    anchor.click();

    document.body.removeChild(anchor); 
    URL.revokeObjectURL(url); 
};