import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import accuracy_score
import joblib

# Load MPU6050 dataset (Replace with your actual CSV file)
data = pd.read_csv("linear\dataset.csv")  

# Features (Accelerometer & Gyroscope readings)
X = data[['Ax', 'Ay', 'Az', 'Gx', 'Gy', 'Gz']]

# Target labels (Jogging = 0, Jumping = 1, Squatting = 2)
y = data['Action']

# Split data into training (80%) and testing (20%) sets
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Train Linear Regression model
model = LinearRegression()
model.fit(X_train, y_train)

# Predict on test data
y_pred = model.predict(X_test)

# Round predictions to nearest integer (0, 1, 2)
y_pred = np.round(y_pred).astype(int)

# Calculate accuracy
accuracy = (y_pred == y_test).mean()
print(f"Model Accuracy: {accuracy * 100:.2f}%")

# Save trained model
joblib.dump(model, "mpu6050_model.pkl")
print("Model saved as 'mpu6050_model.pkl'")
