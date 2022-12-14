from flask import Flask, render_template, request, redirect, url_for, jsonify, session
import requests
from pprint import pprint
from datetime import date
import dummy_data

app = Flask(__name__)

headers = {"Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBhZG1pbi5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY3MzU1MzgxNX0.dms5A4bli52YCrVrZ_kcc0IgDIV7CQTy6b62ebUmDHI"}
orderId = -1 # order state
restaurantId = -1 # verify the current restaurant
meals = []
logged = 0

@app.route("/restaurants", methods=['GET']) 
def list_restaurants():
    response = requests.get('http://localhost:7160/api/Restaurants', headers=headers)
    responseJson = response.json()
    pprint(responseJson)
    return render_template("restaurants.html", data=responseJson)

@app.route("/detail/<id>", methods=['GET'])
def render_detail(id):
    response = requests.get('http://localhost:7160/api/Meals/by-restaurant-id/{id}'.format(id = id))
    if response.ok:
        mealsJson = response.json()
    response = requests.get('http://localhost:7160/api/Feedbacks/Restaurant/{id}'.format(id=id), headers=headers)
    if response.ok:
        feedbacksJson = response.json()

    return render_template("detail.html", data=mealsJson, feedbacks=feedbacksJson)

@app.route("/orders/history", methods=['GET'])
def render_order_history():
    orders = []
    order = {
        "id":0,
        "restaurantName":""
    }
    response = requests.get("http://localhost:7160/api/Orders/User", headers=headers)
    responseJson = response.json()
    if response.ok:
        for ord in responseJson:
            order["id"]=ord["id"]
            response = requests.get("http://localhost:7160/api/Orders/{id}".format(id=ord["id"]), headers=headers)
            if response.ok:
                ordersJson = response.json()
                order["restaurantName"]=ordersJson["restaurantName"]
                pprint(order)
                orders.append(order)
                pprint(orders)
    return render_template("orders_history.html", data=orders)

@app.route("/orders", methods=['GET'])
def render_order():
    price = 0
    for meal in meals:
        price = price + (meal["price"]*meal["amount"])
    price = round(price,1)
    return render_template("order.html", data=meals, price=price)

    
@app.route("/signup", methods=['GET','POST'])
def render_signup():
    data = {
  "email": "",
  "name": "",
  "surname": "",
  "role": "",
  "password": "",
  "confirmPassword": ""
    }
    if request.method=='GET':
        return render_template('register.html')
    elif request.method=='POST':
        data['name']=request.form['fname'] 
        data['surname']=request.form['sname']
        data['email']=request.form['email'] 
        data['role']="customer"
        data['password']=request.form['pw']
        data['confirmPassword']=request.form['pw2']
        response = requests.post('http://localhost:7160/api/Users/register', json=data)
        responseJson = response.json()
        pprint(responseJson)
        if response.ok:
            return redirect(url_for("render_login"))
        else:
            return jsonify(responseJson), 403
        return render_template("login.html")
    
@app.route('/profile', methods=['GET', 'POST'])
def modify_profile():
    return render_template('profile.html')

@app.route("/login", methods=['GET','POST'])
def render_login():
    data = {
  "email": "",
  "password": ""}
    if request.method=='GET':
        return render_template('login.html')
    elif request.method=='POST':
        data['email']=request.form['email']
        data['password']=request.form['pw']
        print(data)
        response = requests.post('http://localhost:7160/api/Users/login', json=data)
        responseJson = response.json()
        pprint(responseJson)
        if response.ok:
            headers['Authorization'] = "Bearer {}".format(responseJson['message'])
            return redirect(url_for("list_restaurants"))
        else:
            return jsonify(responseJson), 403
    return render_template()

@app.route("/admin/login", methods=['GET','POST'])
def render_admin_login():
    data = {
        "email": "",
        "password": ""
    }
    if request.method=='GET':
        return render_template('admin_login.html')
    elif request.method=='POST':
        data['email']=request.form['email']
        data['password']=request.form['pw']
        print(data)
        response = requests.post('http://localhost:7160/api/Users/login', json=data)
        responseJson = response.json()
        pprint(responseJson)
        if response.ok:
            headers['Authorization'] = "Bearer {}".format(responseJson['message'])
            return redirect(url_for("render_admin_home"))
        else:
            return jsonify(responseJson), 403

@app.route("/admin/home", methods=['GET'])
def render_admin_home():
    response = requests.get("http://localhost:7160/api/Restaurants", headers=headers)
    restJson = response.json()
    return render_template("admin_panel.html", restaurants=restJson)

@app.route("/admin/users", methods=['GET'])
def render_show_users():
    response = requests.get("http://localhost:7160/api/Users", headers=headers)
    usersJson = response.json()
    return render_template("show_users.html", users=usersJson)

@app.route("/delete_user/<id>", methods=['GET'])
def delete_user(id):
    response = requests.delete("http://localhost:7160/api/Users?userId={id}".format(id=id), headers=headers)
    if response.ok:
        return redirect(url_for('render_show_users'))

@app.route('/delete_restaurant/<id>', methods=['GET'])
def delete_restaurant(id):
    response = requests.delete("http://localhost:7160/api/Restaurants/{id}".format(id = id), headers=headers)
    pprint(response.status_code)
    if response.ok:
        return redirect(url_for('render_admin_home'))
    else:
        return jsonify(response.json()), 403

@app.route('/delete_food/<id>', methods=['GET'])
def delete_food(id):
    response = requests.delete("http://localhost:7160/api/Meals/{id}".format(id=id), headers=headers)
    if response.ok:
        return jsonify(response.json())

@app.route('/edit_food', methods=['POST'])
def edit_food():
    data = {
        "id":0,
        "name": "",
        "description": "",
        "price":0,
        "mealType":""
    }
    data["id"]=int(request.form["id"])
    data["name"]=request.form["name"]
    data["description"]=request.form["description"]
    data["price"]=float(request.form["price"])
    data["mealType"]=request.form["mealType"]
    response = requests.put("http://localhost:7160/api/Meals", json=data, headers=headers)
    responseJson = response.json()
    if response.ok:
        return jsonify(responseJson)

@app.route('/admin/restaurant/<id>', methods=['GET'])
def edit_restaurant(id):
    response = requests.get("http://localhost:7160/api/Restaurants/{id}/Meals".format(id = id), headers=headers)
    print(response.status_code)
    responseJson = response.json()
    if response.ok:
        pprint(response.json())
        return render_template("admin_restaurant.html", data=responseJson)
    else:
        return jsonify(response.json()), 403

@app.route('/add_to_cart', methods=['POST'])
def add_to_cart():
    global orderId, restaurantId, meals
    data = {
        "orderId": 0,
        "mealId": 0,
        "amount": 1
    }
    meal = {
        "id":0,
        "name":"",
        "mealType":"",
        "price":0,
        "amount":0,
        "orderItemId":0
    }
    data["mealId"]=request.form["mealId"]
    if orderId<0:
        response = requests.get("http://localhost:7160/api/Meals/{id}".format(id = data['mealId']))
        responseJson = response.json()
        if response.ok:
            restaurantId = responseJson["restaurantId"]
            response = requests.post("http://localhost:7160/api/Orders?restaurantId={id}".format(id = restaurantId), headers=headers)
            responseJson = response.json()
            print(responseJson["id"])
            orderId = responseJson["id"]
            pprint(responseJson)
        else:
            return jsonify(response.json()), 403
    data["orderId"]=orderId
    print(data)
    response = requests.post("http://localhost:7160/api/OrderItems", json=data, headers=headers)
    if response.ok:
        print(response.status_code)
        responseJson = response.json()
        meal["id"]=responseJson["meal"]["id"]
        meal["name"]=responseJson["meal"]["name"]
        meal["mealType"]=responseJson["meal"]["mealType"]
        meal["price"]=responseJson["meal"]["price"]
        meal["amount"]=responseJson["amount"]
        meal["orderItemId"]=responseJson["id"]
        meals.append(meal)

    return redirect('/detail/{id}'.format(id=restaurantId))

@app.route('/delete_from_cart/<id>',methods=['GET'])
def delete_from_cart(id):
    response=requests.delete("http://localhost:7160/api/OrderItems/{id}".format(id=id),headers=headers)
    responseJson = response.json()
    pprint(responseJson)
    if response.ok:
        for i in range(len(meals)):
            print(meals[i])
            if meals[i]['orderItemId'] == int(id):
                del meals[i]
                break
        pprint(meals)
    return redirect(url_for('render_order'))

@app.route('/update_cart', methods=['POST'])
def update_cart():
    data={
        "id":0,
        "amount":0
    }
    data["amount"]=request.form["quantity"]
    data["id"]=request.form["orderItemId"]
    response = requests.put("http://localhost:7160/api/OrderItems", json=data, headers=headers)
    responseJson = response.json()
    pprint(responseJson)
    if response.ok:
        for i in range(len(meals)):
            if meals[i]['orderItemId'] == int(data["id"]):
                meals[i]['amount'] = int(data["amount"])
                break
    return redirect(url_for('render_order'))

@app.route('/commit_order',methods=['POST'])
def commit_order():
    global orderId, restaurantId, meals
    orderId = -1
    restaurantId = -1
    meals = []
    return redirect(url_for('render_order'))

@app.route('/get_feedback/<id>')
def get_feedback(id):
    response = requests.get("http://localhost:7160/api/Feedbacks/Meal/{id}".format(id=id), headers=headers)
    responseJson = response.json()
    return jsonify(responseJson)

@app.route('/logout', methods=['GET'])
def logout():
    global headers, orderId, restaurantId, meals
    headers["Authorization"] = ""
    orderId = -1 
    restaurantId = -1 
    meals = []
    return redirect(url_for('list_restaurants'))
