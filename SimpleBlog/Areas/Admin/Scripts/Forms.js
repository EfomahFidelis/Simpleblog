$(document).ready(function () {

    // search for every single "a tag" that has a data-post on it
    $("a[data-post]").click(function (e) {
        e.preventDefault();

        // alias out the jquery object of our 
        // context into a variable $this for easy of access
        var $this = $(this);
        var message = $this.data("post");

        // If there is a message and we open up
        // a dialog box to confirm and 'No' action
        // is clicked, exit don't anything
        if (message && !confirm(message))
            return;

        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href"))
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();
    });

    $("[data-slug]").each(function() {
        var $this = $(this);
        var $sendSlugFrom = $($this.data("slug"));

        $sendSlugFrom.keyup(function() {
            var slug = $sendSlugFrom.val();
            slug = slug.replace(/[^a-zA-Z0-9\s]/g, "");
            slug = slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-");

            if(slug.charAt(slug.length - 1) == "-")
                slug = slug.substr(0, slug.length - 1)

            $this.val(slug);
        });   
    });

});