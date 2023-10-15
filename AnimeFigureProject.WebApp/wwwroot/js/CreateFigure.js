var selectedOrigins = [];
document.getElementById("originSelect").onchange = function (e)
{

    var newElement = document.createElement("button");

    newElement.id = this.selectedIndex;
    newElement.classList.add("btn", "btn-primary");
    newElement.textContent = this[this.selectedIndex].text;

    newElement.onclick = function ()
    {

        document.getElementById("Choosen").removeChild(this);
        selectedOrigins.splice(selectedOrigins.indexOf(this.textContent), 1)
        UpdateSelectedOrigins();

    }

    if (!document.getElementById(this.selectedIndex) && this.selectedIndex > 0)
    {

        document.getElementById("Choosen").appendChild(newElement);
        selectedOrigins.push(newElement.textContent);
        UpdateSelectedOrigins();

    }

}

function UpdateSelectedOrigins()
{

    document.getElementById('selectedOrigins').value = selectedOrigins;

}

function PreviewImages(event)
{

    document.getElementById("ImagePreview").innerHTML = '';
    var ImageFiles = event.target.files;

    for (var i = 0; i < ImageFiles.length; i++)
    {

        var CurrentFile = ImageFiles[i];
        var Reader = new FileReader();

        Reader.onload = function (e)
        {

            var Image = document.createElement('img');
            Image.src = e.target.result;
            document.getElementById("ImagePreview").appendChild(Image);

        }

        Reader.readAsDataURL(CurrentFile);

    }

}