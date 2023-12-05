from flask import Flask, render_template, request


app = Flask(__name__)

@app.route('/hello/', methods = ['POST', 'GET'])
def index():
    if request.method == 'POST':
        user = request.form['name']
        return render_template('hello.html', name=user)
    else:
        user = None
        return  render_template('hello.html', name=user)
    