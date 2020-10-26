function removeLine() {
    setTimeout(function () {
        document.querySelector('table tr:last-child .ant-select-selection-item').innerHTML = ''
    }, 70)
}