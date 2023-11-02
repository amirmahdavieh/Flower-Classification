# Flower Classification using Artificial Neural Networks

## Overview

This project involves creating an algorithm to classify flowers into three types (Lily or Iris) using Artificial Neural Networks (ANN). The dataset consists of 150 samples for each flower type, with four features for each sample (sepal length, sepal width, petal length, petal width). The data, available at [UCI Machine Learning Repository](https://archive.ics.uci.edu/ml/machine-learning-databases/iris/iris.data), will be used for the project.

## Artificial Neural Network (ANN) Structure

The basic structures in this project are Artificial Neurons, forming the foundation of Artificial Neural Networks (ANNs). ANNs are employed for classification, clustering, and prediction tasks.

The project aims to create a simple neural network to solve the given flower classification problem.

## Implementation Steps

### a) Artificial Neuron Class

- Create a `Neuron` class to hold inputs and weights using appropriate data structures.
- Initialize all weights to random positive values within the [0, 1] range.
- Implement a method to perform calculations and necessary operations (calculate neuron output).

### b) Neural Network Class

- Create a `NeuralNetwork` class containing three neurons.
- Implement a training method that compares expected values with neuron outputs.
- Before training, scale all input data to the [0, 1] range.
- Simplified learning rule: Set the expected value to 1 for the corresponding neuron and 0 for others.
- Train the network using the given learning rate (λ) for 50 epochs.

### c) Training and Evaluation

- Train the network for 50 epochs with a learning rate (λ) of 0.01.
- After training, input data and calculate output values without changing weights.
- Count the correctly classified instances, calculate accuracy, and print the results.
- Repeat the process for 20 and 100 epochs with learning rates of 0.005 and 0.025.

### d) Experimentation

- Repeat the experiments for λ values of 0.005, 0.01, and 0.025, and for 20, 50, and 100 epochs.
- Record accuracy values in a 3x3 table (epochs vs. λ) for observations.
- Repeat the experiments two more times to observe changes in success values.

## Conclusion

Observations indicate that changing the learning rate and epochs affects the accuracy of the neural network in classifying flowers. Further experimentation may provide insights into the optimal parameters for this specific problem.

Feel free to contribute to the project and explore additional improvements! If you encounter any issues or have suggestions, please open an issue.
