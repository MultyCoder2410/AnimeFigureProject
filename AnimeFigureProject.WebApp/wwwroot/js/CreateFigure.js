var selectedOrigins = [];
var uploadedImages = [];

function AddOrigin()
{

    var inputElement = document.getElementById('originInput')

    if (inputElement.value.length > 0)
    {

        var newElement = document.createElement("button");

        newElement.classList.add("btn", "btn-primary");
        newElement.textContent = inputElement.value;

        newElement.onclick = function () {

            document.getElementById("Choosen").removeChild(this);
            selectedOrigins.splice(selectedOrigins.indexOf(this.textContent), 1)
            document.getElementById('selectedOrigins').value = selectedOrigins;

        }

        document.getElementById("Choosen").appendChild(newElement);
        selectedOrigins.push(newElement.textContent);
        document.getElementById('selectedOrigins').value = selectedOrigins;

        inputElement.value = '';

    }

}

function PreviewImages(event)
{

    var imageHtml = document.getElementById("MainImage").innerHTML;
    document.getElementById("MainImage").innerHTML = '';
    document.getElementById("SubImagePreview").innerHTML += imageHtml;

    var reader = new FileReader();

    reader.onload = function (e)
    {

        var image = document.createElement('img');
        image.src = e.target.result;
        document.getElementById("MainImage").appendChild(image);

    }

    reader.readAsDataURL(event.target.files[0]);
    uploadedImages.push(event.target.files[0]);

}

function UploadData(form)
{

    const formData = new FormData(form);

    for (var i = 0; i < uploadedImages.length; i++)
        formData.append('imagesData', uploadedImages[i]);

    const xhr = new XMLHttpRequest();
    xhr.open('POST', '/Catalog/Create');
    xhr.send(formData);

    return false;

}

function ChangeCategory()
{

    document.getElementById("categoryInput").value = document.getElementById("categoryDropdown").value;
    document.getElementById("categoryDropdown").selectedIndex = -1;

}

function ChangeOrigin()
{

    document.getElementById("originInput").value = document.getElementById("originSelect").value;
    document.getElementById("originSelect").selectedIndex = -1;

}

function ChangeBrand()
{

    document.getElementById("brandInput").value = document.getElementById("brandDropdown").value;
    document.getElementById("brandDropdown").selectedIndex = -1;

}