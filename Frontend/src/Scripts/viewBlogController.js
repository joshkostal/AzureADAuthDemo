const blogUrl = 'https://localhost:44311/api/blog'
const postContainer = document.getElementById('blogPosts')
const postDiv = document.getElementById('post_0')

let i = 1

function getBlog() {
    let request = new XMLHttpRequest()
    request.open('GET', blogUrl, true)
    request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')

    request.onreadystatechange = function() {
        if (request.readyState == 4 && request.status == 200) {
            var blogPosts = JSON.parse(request.response)

            blogPosts.forEach(post => {
                postContainer.appendChild(createPostNode(post))
                i++
            });
            postDiv.remove()
        }
    }
    request.send()
}

function createPostNode(post) {
    var newPost = postDiv.cloneNode(true)
    var newTitle = newPost.querySelector("#post_0_title")
    var newImage = newPost.querySelector("#post_0_image")
    var newBody = newPost.querySelector("#post_0_body")

    newTitle.appendChild(document.createTextNode(post.item1))
    newImage.src = post.item2
    newBody.appendChild(document.createTextNode(post.item3))

    newPost.id = `post_${i}`
    newTitle.id = `post_${i}_title`
    newImage.id = `post_${i}_image`
    newBody.id = `post_${i}_body`

    return newPost
}