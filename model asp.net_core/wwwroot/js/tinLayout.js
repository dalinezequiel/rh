const menu = document.querySelectorAll("nav ul li a");
for (var i = 0; i < menu.length; i++) {
    /*menu[i].addEventListener('click', function () {
        alert("Click! Index: " + i);
        if (i == 12) {
            menu[2].style.backgroundColor = "#eeeeee";
            menu[2].style.color = "blue";
        }
        menu[i].style.backgroundColor = "#eeeeee";
        menu[i].style.color = "blue";
    })*/
}

const graf = document.getElementsByTagName('canvas');
/*new Chart(graf[0], {
    type: 'bar',
    data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});*/

new Chart(graf[0], {
    type: 'line',
    data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

new Chart(graf[1], {
    type: 'radar',
    data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});
