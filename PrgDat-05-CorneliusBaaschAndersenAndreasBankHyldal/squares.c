void arrsum(int n, int arr[], int *sump) {
  int i;
  int sum;
  i = 0;
  sum = 0;
  while (i < n) {
    sum = sum + arr[i];
    i = i + 1;
  }
  *sump = sum;
}

void squares(int n, int arr[]) {
  int i;
  i = 0;
    for (i = 0; i < n; i = i + 1)
    arr[i] = i * i;
}

void main(int n) {
    int arr[20];
    int sum;
    sum = 0;
    squares(n, arr);
    arrsum(n, arr, &sum);
    print sum;
}