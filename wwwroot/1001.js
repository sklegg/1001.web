window.scrollToTop = (element) => {
    if (document.getElementById(element))
        document.getElementById(element).scrollIntoView(false);
}