function findByNameInList(inputFieldId, listBlockId, tagNameOfOneItemInList = "li", tagNameOfNameElement = "span") {
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById(inputFieldId);
    filter = input.value.toUpperCase();
    ul = document.getElementById(listBlockId);
    li = document.querySelectorAll('#' + listBlockId + ' > ' + tagNameOfOneItemInList);
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName(tagNameOfNameElement)[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}