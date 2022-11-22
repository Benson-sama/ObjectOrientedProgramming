namespace AdvancedTechniques
{
    public class Complex
    {
        private double real;
        private double imag;

        public Complex(double real = 3.14, double imag = 1.2)
        {
            this.real = real;
            this.imag = imag;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            Complex realc1 = c1 ?? new Complex();
            return new Complex(c1.real + c2.real, c1.imag + c2.imag);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            Complex realc1 = c1 ?? new Complex();
            return new Complex(c1.real - c2.real, c1.imag - c2.imag);
        }

        public override string ToString()
        {
            return $"real: {this.real} | imag: {this.imag}";
        }
    }
}
